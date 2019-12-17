using MongoDB.Driver;
using System;
using System.Threading.Tasks;

namespace IntelligentCheckout.Backend.Domain.Repositories
{
    /// <summary>
    /// Repósitorio somente de leitura de Pessoas
    /// </summary>
    public interface IPessoasLeitura
    {
        /// <summary>
        /// Obtém uma pessoa pelo Id
        /// </summary>
        /// <param name="id">Id da Pessoa</param>
        /// <returns>Pessoa</returns>
        Task<Models.Pessoa> ObterPorId(Guid id);
    }

    /// <summary>
    /// Repósitorio somente de escrita de Pessoas
    /// </summary>
    public interface IPessoasEscrita
    {
        /// <summary>
        /// Insere uma Pessoa
        /// </summary>
        /// <param name="pessoa">Pessoa</param>
        /// <returns>Task</returns>
        Task Inserir(Models.Pessoa pessoa);
    }

    internal class Pessoas : IPessoasLeitura, IPessoasEscrita
    {
        private readonly IntelligentCheckoutDB _db;
        public Pessoas(IntelligentCheckoutDB db)
            => this._db = db;

        public async Task Inserir(Models.Pessoa pessoa)
            => await this._db.Pessoas.InsertOneAsync(pessoa);

        public async Task<Models.Pessoa> ObterPorId(Guid id)
        {
            var pessoas  = await this._db.Pessoas.FindAsync(p => p.Id == id);
            var pessoa = await pessoas.FirstOrDefaultAsync();
            pessoa?.DeixarApenasUmaFotosBase64();
            return pessoa;
        }
    }
}
