using System;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace IntelligentCheckout.Backend.Domain.Repositories
{
    /// <summary>
    /// Repósitorio somente de leitura do Carrinho de Compras
    /// </summary>
    public interface ICarrinhosLeitura
    {
        /// <summary>
        /// Obtém um carrinho pelo Id da Sessão
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <returns>Carrinho de Compra</returns>
        Task<Models.Carrinho> ObterPorIdSessao(Guid idSessao);
    }

    /// <summary>
    /// Repósitorio somente de escrita do Carrinho de Compras
    /// </summary>
    public interface ICarrinhosEscrita
    {
        /// <summary>
        /// Cria uma sessão
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <returns>Carrinho de Compra</returns>
        Task<Models.Carrinho> CriarSessao(Guid idSessao);

        /// <summary>
        /// Atualiza um carrinho de compra
        /// </summary>
        /// <param name="carrinho">Carrinho de Compra</param>
        /// <returns>Carrinho de Compra</returns>
        Task<Models.Carrinho> Atualizar(Models.Carrinho carrinho);
        
        /// <summary>
        /// Exclui uma sessão
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <returns>Task</returns>
        Task ExcluirPorIdSessao(Guid idSessao);
    }

    internal class Carrinhos : ICarrinhosLeitura, ICarrinhosEscrita
    {
        private readonly IntelligentCheckoutDB _db;
        public Carrinhos(IntelligentCheckoutDB db)
            => this._db = db;

        public async Task<Models.Carrinho> Atualizar(Models.Carrinho carrinho)
        {
            var filter = Builders<Models.Carrinho>.Filter.Eq(c => c.IdSessao, carrinho.IdSessao);
            var update = Builders<Models.Carrinho>.Update.Set(c => c.ItensDeCompra, carrinho.ItensDeCompra);
            var result = await this._db.Carrinhos.FindOneAndUpdateAsync<Models.Carrinho>(filter, update);
            return carrinho;
        }

        public async Task<Models.Carrinho> CriarSessao(Guid idSessao)
        {
            var carrinho = new Models.Carrinho(idSessao);
            await this._db.Carrinhos.InsertOneAsync(carrinho);
            return carrinho;
        }

        public async Task ExcluirPorIdSessao(Guid idSessao)
            => await this._db.Carrinhos.FindOneAndDeleteAsync(c => c.IdSessao == idSessao);

        public async Task<Models.Carrinho> ObterPorIdSessao(Guid idSessao)
        {
            var carrinhos = await this._db.Carrinhos.FindAsync(c => c.IdSessao == idSessao);
            return await carrinhos.FirstOrDefaultAsync();
        }
    }
}
