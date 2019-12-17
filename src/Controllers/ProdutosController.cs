using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace IntelligentCheckout.Backend.Controllers
{
    /// <summary>
    /// API de Produtos
    /// </summary>
    [ApiController]
    [Route("api/produtos")]
    public class ProdutosController : ControllerBase
    {
        private readonly Domain.Repositories.IProdutosLeitura _produtosLeitura;
        private readonly Domain.Repositories.IProdutosEscrita _produtosEscrita;

        public ProdutosController(Domain.Repositories.IProdutosLeitura produtosLeitura, 
                                  Domain.Repositories.IProdutosEscrita produtosEscrita)
        {
            this._produtosLeitura = produtosLeitura;
            this._produtosEscrita = produtosEscrita;
        }

#if DEBUG
        /// <summary>
        /// Inserir Produto (Apenas para fins de carregamento de base)
        /// </summary>
        /// <returns>Produto</returns>
        [HttpPost]
        public async Task<Models.Resposta<Domain.Models.Produto>> Post(Domain.Models.Produto produto)
        {
            await this._produtosEscrita.Inserir(produto);
            return new Models.Resposta<Domain.Models.Produto>(produto);
        }
#endif

        /// <summary>
        /// Obtém um produto pelo Id
        /// </summary>
        /// <returns>Produto</returns>
        [HttpGet("{idProduto}")]
        public async Task<Models.Resposta<Domain.Models.Produto>> Get(Guid idProduto)
        {
            var produto = await this._produtosLeitura.ObterPorId(idProduto);
            return new Models.Resposta<Domain.Models.Produto>(produto);
        }

        /// <summary>
        /// Obtém todos os produtos
        /// </summary>
        /// <returns>Produtos</returns>
        [HttpGet]
        public async Task<Models.Resposta<IEnumerable<Domain.Models.Produto>>> Get()
        {
            var produtos = await this._produtosLeitura.ListarTodos();
            return new Models.Resposta<IEnumerable<Domain.Models.Produto>>(produtos);
        }

        /// <summary>
        /// Efetua uma busca por produtos
        /// </summary>
        /// <returns>Produtos</returns>
        [HttpGet("buscar")]
        public async Task<Models.Resposta<IEnumerable<Domain.Models.Produto>>> Get([FromQuery]string termo)
        {
            var produtos = await this._produtosLeitura.Buscar(termo);
            return new Models.Resposta<IEnumerable<Domain.Models.Produto>>(produtos);
        }
    }
}
