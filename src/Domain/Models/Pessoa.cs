using System;
using System.Collections.Generic;
using System.Linq;

namespace IntelligentCheckout.Backend.Domain.Models
{
    public class Pessoa
    {
        /// <summary>
        /// Id da Pessoa
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Nome da Pessoa
        /// </summary>
        public string Nome { get; set; }

        /// <summary>
        /// Ids dos Rostos
        /// </summary>
        public IList<FotoRosto> FotosDoRosto { get; set; }

        /// <summary>
        /// Pessoa
        /// </summary>
        public Pessoa() { }

        /// <summary>
        /// Cria uma pessoa
        /// </summary>
        /// <param name="id">Id da Pessoa</param>
        /// <param name="nome">Nome da Pessoa</param>
        /// <param name="fotosDoRosto">Lista de fotos do Rosto</param>
        public Pessoa(Guid id, string nome, IList<FotoRosto> fotosDoRosto)
            : this()
        {
            this.Id = id;
            this.Nome = nome;
            this.FotosDoRosto = fotosDoRosto;
        }

        /// <summary>
        /// Remove fotos base64. 
        /// </summary>
        public void RemoverFotosBase64()
            => this.FotosDoRosto = null;

        /// <summary>
        /// Como uma pessoa pode ter várias fotos, escolhe uma aleatóriamente dentre todas as cadastradas.
        /// </summary>
        public void DeixarApenasUmaFotosBase64()
            => this.FotosDoRosto = this.FotosDoRosto
                                       .OrderBy(f => DateTime.Now)
                                       .Take(1)
                                       .ToList();

        /// <summary>
        /// Foto do Rosto
        /// </summary>
        public class FotoRosto
        {
            /// <summary>
            /// Id da Foto no Serviço Cognitivo
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Foto em Base64
            /// </summary>
            public string FotoEmBase64 { get; set; }

            /// <summary>
            /// Fotos do Rosto
            /// </summary>
            public FotoRosto() { }

            /// <summary>
            /// Fotos do Rosto
            /// </summary>
            /// <param name="id">Id da Foto</param>
            /// <param name="fotoEmBase64">Foto do Rosto em base64</param>
            public FotoRosto(Guid id, string fotoEmBase64)
            {
                this.Id = id;
                this.FotoEmBase64 = fotoEmBase64;
            }
        }
    }
}
