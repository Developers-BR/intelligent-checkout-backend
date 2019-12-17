using IntelligentCheckout.Backend.Infra;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IntelligentCheckout.Backend.Controllers
{
    /// <summary>
    /// API de Compras
    /// </summary>
    [ApiController]
    [Route("api/compras")]
    public class ComprasController : ControllerBase
    {
        private readonly Domain.Repositories.IComprasLeitura _comprasLeitura;
        private readonly Domain.Services.ICompras _compras;

        public ComprasController(Domain.Repositories.IComprasLeitura comprasLeitura, Domain.Services.ICompras compras)
        {
            this._comprasLeitura = comprasLeitura;
            this._compras = compras;
        }

        /// <summary>
        /// Finaliza a compra
        /// </summary>
        /// <param name="viewmodel">Compra</param>
        /// <returns>Dados da compra</returns>
        [HttpPost("finalizar")]
        public async Task<Models.Resposta<Domain.Models.Compra>> Finalizar([FromBody]Models.FinalizarCompra viewmodel)
        {
            if (!ModelState.IsValid)
                return ModelState.ObterResposta<Domain.Models.Compra>();

            var compra = await this._compras.Finalizar(viewmodel.IdSessao, viewmodel.FotoDoRostoEmBase64);
            return new Models.Resposta<Domain.Models.Compra>(compra);
        }

        /// <summary>
        /// Lista as compras de uma pessoa
        /// </summary>
        /// <param name="idPessoa">Id Pessoa</param>
        /// <returns>Compras</returns>
        [HttpGet("pessoa/{idPessoa}")]
        public async Task<Models.Resposta<IEnumerable<Domain.Models.Compra>>> ListarPorPessoa(Guid idPessoa)
        {
            var compras = await this._comprasLeitura.ListarPorPessoa(idPessoa);
            return new Models.Resposta<IEnumerable<Domain.Models.Compra>>(compras);
        }
    }
}
