using System;
using System.Threading.Tasks;

namespace IntelligentCheckout.Backend.Domain.Services
{
    public interface ICarrinhos
    {
        Task<Models.Carrinho> AtualizarQuantidadeDeUmProduto(Guid idSessao, Guid idProduto, int quantidade);
        Task ExcluirPorIdSessao(Guid idSessao);
        Task<Models.Carrinho> IncluirItemDeCompra(Guid idSessao, Guid idProduto, int quantidade);
        Task<Models.Carrinho> RemoverItemDeCompra(Guid idSessao, Guid idProduto);
    }

    internal class Carrinhos : ICarrinhos
    {
        private readonly Repositories.ICarrinhosLeitura _carrinhosLeitura;
        private readonly Repositories.ICarrinhosEscrita _carrinhosEscrita;

        private readonly Repositories.IProdutosLeitura _produtosLeitura;

        public Carrinhos(Repositories.ICarrinhosLeitura carrinhosLeitura,
                         Repositories.ICarrinhosEscrita carrinhosEscrita,
                         Repositories.IProdutosLeitura produtosLeitura)
        {
            this._carrinhosLeitura = carrinhosLeitura;
            this._carrinhosEscrita = carrinhosEscrita;

            this._produtosLeitura = produtosLeitura;
        }

        /// <summary>
        /// Incluir um ítem de compra no Carrinho
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <param name="idProduto">Id do Produto</param>
        /// <param name="quantidade">Quantidade</param>
        /// <returns>Carrinho</returns>
        public async Task<Models.Carrinho> IncluirItemDeCompra(Guid idSessao, Guid idProduto, int quantidade)
        {
            var carrinho = await this.CriarOuObterPorSessao(idSessao);
            var produto = await this._produtosLeitura.ObterPorId(idProduto);
            carrinho.AdicionarItemDeCompra(produto, quantidade);

            return await this._carrinhosEscrita.Atualizar(carrinho);
        }

        /// <summary>
        /// Remove um ítem de compra do Carrinho
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <param name="idProduto">Item de Compra</param>
        /// <returns>Carrinho</returns>
        public async Task<Models.Carrinho> RemoverItemDeCompra(Guid idSessao, Guid idProduto)
        {
            var carrinho = await this.CriarOuObterPorSessao(idSessao);
            carrinho.RemoverItemDeCompra(idProduto);

            return await this._carrinhosEscrita.Atualizar(carrinho);
        }

        /// <summary>
        /// Atualiza a quantidade de um ítem de compra do Carrinho
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <param name="idProduto">id do produto</param>
        /// <param name="quantidade">Quantidade</param>
        /// <returns>Carrinho</returns>
        public async Task<Models.Carrinho> AtualizarQuantidadeDeUmProduto(Guid idSessao, Guid idProduto, int quantidade)
        {
            var carrinho = await this.CriarOuObterPorSessao(idSessao);
            carrinho.AtualizarQuantidadeDeUmProduto(idProduto, quantidade);

            return await this._carrinhosEscrita.Atualizar(carrinho);
        }

        /// <summary>
        /// Remove o Carrinho
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        public async Task ExcluirPorIdSessao(Guid idSessao)
            => await this._carrinhosEscrita.ExcluirPorIdSessao(idSessao);

        private async Task<Models.Carrinho> CriarOuObterPorSessao(Guid idSessao)
        {
            var carrinho = await this._carrinhosLeitura.ObterPorIdSessao(idSessao);
            if (carrinho != null) return carrinho;
            return await this._carrinhosEscrita.CriarSessao(idSessao);
        }
    }
}