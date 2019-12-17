using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace IntelligentCheckout.Backend.Domain.Repositories
{
    /// <summary>
    /// Repósitorio somente de leitura de Compras
    /// </summary>
    public interface IComprasLeitura
    {
        /// <summary>
        /// Listar as compras de uma pessoa pelo id
        /// </summary>
        /// <param name="idPessoa">Id da Pessoa</param>
        /// <returns>Lista de Compras</returns>
        Task<IEnumerable<Models.Compra>> ListarPorPessoa(Guid idPessoa);
    }

    /// <summary>
    /// Repósitorio somente de escrita de Compras
    /// </summary>
    public interface IComprasEscrita
    {
        /// <summary>
        /// Inserir uma compra
        /// </summary>
        /// <param name="compra">Compra</param>
        /// <returns>Task</returns>
        Task Inserir(Models.Compra compra);
    }

    internal class Compras : IComprasLeitura, IComprasEscrita
    {
        private readonly IntelligentCheckoutDB _db;
        public Compras(IntelligentCheckoutDB db)
            => this._db = db;

        public async Task Inserir(Models.Compra compra)
            => await this._db.Compras.InsertOneAsync(compra);

        public async Task<IEnumerable<Models.Compra>> ListarPorPessoa(Guid idPessoa)
        {
            var compras = await this._db.Compras.FindAsync(c => c.Pessoa.Id == idPessoa);
            return compras.ToList();
        }
    }
}
