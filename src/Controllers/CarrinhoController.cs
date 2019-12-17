using System;
using System.Threading.Tasks;
using IntelligentCheckout.Backend.Infra;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentCheckout.Backend.Controllers
{
    /// <summary>
    /// API de Carrinho de Compras.
    /// </summary>
    [ApiController]
    [Route("api/carrinho/{idSessao}")]
    public class CarrinhoController : ControllerBase
    {
        private readonly Domain.Repositories.ICarrinhosLeitura _carrinhosLeitura;
        private readonly Domain.Services.ICarrinhos _carrinhos;

        /// <summary>
        /// Criação do Controller
        /// </summary>
        /// <param name="carrinhosLeitura">Repositório de Leitura do Carrinho</param>
        /// <param name="carrinhos">Serviço de Carrinho</param>
        public CarrinhoController(Domain.Repositories.ICarrinhosLeitura carrinhosLeitura, 
                                  Domain.Services.ICarrinhos carrinhos)
        {
            this._carrinhosLeitura = carrinhosLeitura;
            this._carrinhos = carrinhos;
        }

        /// <summary>
        /// Listar Itens de Compra no Carrinho
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <returns>Carrinho de Compra</returns>
        [HttpGet]
        public async Task<Models.Resposta<Domain.Models.Carrinho>> Get(Guid idSessao)
        {
            var carrinho = await this._carrinhosLeitura.ObterPorIdSessao(idSessao);
            return new Models.Resposta<Domain.Models.Carrinho>(carrinho);
        }

        /// <summary>
        /// Incluir Item de Compra no Carrinho
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <param name="itemDeCompra">Item de Compra</param>
        /// <returns>Carrinho de Compra</returns>
        [HttpPost]
        public async Task<Models.Resposta<Domain.Models.Carrinho>> Post(Guid idSessao, 
                                                                        [FromBody]Models.ItemDeCompra itemDeCompra)
        {
            if (!ModelState.IsValid)
                return ModelState.ObterResposta<Domain.Models.Carrinho>();

            var carrinho = await this._carrinhos.IncluirItemDeCompra(idSessao, itemDeCompra.IdProduto, itemDeCompra.Quantidade);
            return new Models.Resposta<Domain.Models.Carrinho>(carrinho);
        }

        /// <summary>
        /// Atualiza a quantidade de um produto do Carrinho de Compra
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <param name="itemDeCompra">Item de Compra</param>
        /// <returns>Carrinho de Compra</returns>
        [HttpPatch]
        public async Task<Models.Resposta<Domain.Models.Carrinho>> Patch(Guid idSessao, 
                                                                         [FromBody]Models.ItemDeCompra itemDeCompra)
        {
            if (!ModelState.IsValid)
                return ModelState.ObterResposta<Domain.Models.Carrinho>();

            var carrinho = await this._carrinhos.AtualizarQuantidadeDeUmProduto(idSessao, itemDeCompra.IdProduto, itemDeCompra.Quantidade);
            return new Models.Resposta<Domain.Models.Carrinho>(carrinho);
        }

        /// <summary>
        /// Remover produto do Carrinho de Compra
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <param name="idProduto">Id do Produto</param>
        /// <returns>Carrinho de Compra</returns>
        [HttpDelete("{idProduto}")]
        public async Task<Models.Resposta<Domain.Models.Carrinho>> Delete(Guid idSessao, 
                                                                          Guid idProduto)
        {
            var carrinho = await this._carrinhos.RemoverItemDeCompra(idSessao, idProduto);
            return new Models.Resposta<Domain.Models.Carrinho>(carrinho);
        }

        /// <summary>
        /// Limpar Carrinho de Compra
        /// </summary>
        /// <param name="idSessao">Id da Sessão</param>
        /// <returns>Resposta</returns>
        [HttpDelete]
        public async Task<Models.Resposta> Delete(Guid idSessao)
        {
            await this._carrinhos.ExcluirPorIdSessao(idSessao);
            return Models.Resposta.OK;
        }
    }
}
