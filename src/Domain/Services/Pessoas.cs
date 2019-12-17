using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace IntelligentCheckout.Backend.Domain.Services
{
    public interface IPessoas
    {
        Task<Models.Pessoa> Inserir(string nome, IList<string> fotosDoRostoEmBase64);
        Task<Models.Pessoa> ObterPelaFotoDoRosto(string fotoDoRostoEmBase64, double confidence = 0.6);
    }

    internal class Pessoas : IPessoas
    {
        private readonly IFaceClient _faceClient;
        private readonly Configuracoes _configuracoes;
        private readonly Repositories.IPessoasLeitura _pessoasLeitura;
        private readonly Repositories.IPessoasEscrita _pessoasEscrita;

        public Pessoas(Configuracoes configuracoes,
                       Repositories.IPessoasLeitura pessoasLeitura,
                       Repositories.IPessoasEscrita pessoasEscrita)
        {
            this._configuracoes = configuracoes;
            this._pessoasLeitura = pessoasLeitura;
            this._pessoasEscrita = pessoasEscrita;


            this._faceClient = new FaceClient(new ApiKeyServiceClientCredentials(this._configuracoes.Chave))
            {
                Endpoint = this._configuracoes.Endpoint
            };
        }

        public async Task<Models.Pessoa> Inserir(string nome, IList<string> fotosDoRostoEmBase64)
        {
            //TODO: Verificar se já não existe a pessoa
            var person = await this._faceClient.PersonGroupPerson.CreateAsync(this._configuracoes.PersonGroupId, nome);

            var fotosDoRosto = new List<Models.Pessoa.FotoRosto>(fotosDoRostoEmBase64.Count);
            foreach (var fotoDoRostoEmBase64 in fotosDoRostoEmBase64)
            {
                var stream = this.ToStream(fotoDoRostoEmBase64);
                var persistedFace = await this._faceClient.PersonGroupPerson.AddFaceFromStreamAsync(this._configuracoes.PersonGroupId, person.PersonId, stream);
                fotosDoRosto.Add(new Models.Pessoa.FotoRosto(persistedFace.PersistedFaceId, fotoDoRostoEmBase64));
            }

            this.FireAndForgetLongRunning(() => this._faceClient.PersonGroup.TrainAsync(this._configuracoes.PersonGroupId));

            var pessoa = new Models.Pessoa(person.PersonId, nome, fotosDoRosto);
            await this._pessoasEscrita.Inserir(pessoa);

            return pessoa;
        }

        public async Task<Models.Pessoa> ObterPelaFotoDoRosto(string fotoDoRostoEmBase64, double confidence = 0.6D)
        {
            var stream = this.ToStream(fotoDoRostoEmBase64);
            var detectedFaces = await this._faceClient.Face.DetectWithStreamAsync(stream);
            var faceIds = detectedFaces.Where(df => df.FaceId.HasValue).Select(df => df.FaceId.Value).ToList();
            var result = await this._faceClient.Face.IdentifyAsync(faceIds, this._configuracoes.PersonGroupId);

            var candidates = new List<IdentifyCandidate>();
            foreach (var item in result)
                candidates.AddRange(item.Candidates);

            var candidate = candidates.FirstOrDefault(c => c.Confidence >= confidence);
            if (candidate == null) return null;

            var pessoa = await this._pessoasLeitura.ObterPorId(candidate.PersonId);
            return pessoa;
        }

        private void FireAndForgetLongRunning(Func<Task> function)
            => Task.Factory
                   .StartNew(function, TaskCreationOptions.LongRunning)
                   .ConfigureAwait(false);

        private Stream ToStream(string base64)
        {
            var parts = base64.Split(',');
            var index = parts.Length - 1;
            var byteArray = Convert.FromBase64String(parts[index]);
            var stream = new MemoryStream(byteArray);
            return stream;
        }

        public class Configuracoes
        {
            public string Chave { get; }
            public string Endpoint { get; }
            public string PersonGroupId { get; }

            public Configuracoes() { }
            public Configuracoes(string chave, string endpoint, string personGroupId)
            {
                this.Chave = chave;
                this.Endpoint = endpoint;
                this.PersonGroupId = personGroupId;
            }
        }
    }
}
