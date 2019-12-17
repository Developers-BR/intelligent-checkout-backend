using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace IntelligentCheckout.Backend.Domain.Repositories
{
    /// <summary>
    /// Repósitorio somente de leitura de Produtos
    /// </summary>
    public interface IProdutosLeitura
    {
        /// <summary>
        /// Lista todos os produtos
        /// </summary>
        /// <returns>Lista de Produtos</returns>
        Task<IEnumerable<Models.Produto>> ListarTodos();

        /// <summary>
        /// Busca por produtos a partir de um termo
        /// </summary>
        /// <param name="termo">Termo a ser buscado</param>
        /// <returns>Lista de Produtos</returns>
        Task<IEnumerable<Models.Produto>> Buscar(string termo);

        /// <summary>
        /// Obter Produto por id
        /// </summary>
        /// <param name="id">Id do produto</param>
        /// <returns>Produto</returns>
        Task<Models.Produto> ObterPorId(Guid id);
    }

    /// <summary>
    /// Repósitorio somente de escrita de Produtos
    /// </summary>
    public interface IProdutosEscrita
    {
        /// <summary>
        /// Insere um produto
        /// </summary>
        /// <param name="produto">Produto</param>
        /// <returns>Task</returns>
        Task Inserir(Models.Produto produto);
    }

    internal class Produtos : IProdutosLeitura, IProdutosEscrita
    {
        private readonly IntelligentCheckoutDB _db;
        public Produtos(IntelligentCheckoutDB db)
            => this._db = db;

        public async Task<IEnumerable<Models.Produto>> Buscar(string termo)
        {
            var produtos = await this.ListarTodos();
            return produtos.Where(p => p.Nome.ToUpper().Contains(termo, StringComparison.InvariantCultureIgnoreCase) ||
                                       p.Descricao.ToUpper().Contains(termo, StringComparison.InvariantCultureIgnoreCase));
        }

        public async Task Inserir(Models.Produto produto)
            => await this._db.Produtos.InsertOneAsync(produto);

        public async Task<IEnumerable<Models.Produto>> ListarTodos()
        {
            var produtos = await this._db.Produtos.FindAsync(Builders<Models.Produto>.Filter.Empty);
            return produtos.ToList();
        }

        public async Task<Models.Produto> ObterPorId(Guid id)
        {
            var produtos = await this._db.Produtos.FindAsync(p => p.Id == id);
            return await produtos.FirstOrDefaultAsync();
        }
    }
}
