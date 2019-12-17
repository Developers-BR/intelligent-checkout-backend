using IntelligentCheckout.Backend.Infra;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace IntelligentCheckout.Backend.Controllers
{
    /// <summary>
    /// API de Pessoas
    /// </summary>
    [ApiController]
    [Route("api/pessoas")]
    public class PessoasController : ControllerBase
    {
        private readonly Domain.Services.IPessoas _pessoas;
        public PessoasController(Domain.Services.IPessoas pessoas)
            => this._pessoas = pessoas;

        /// <summary>
        /// Obter pessoa pela Rosto
        /// </summary>
        /// <returns>Pessoa</returns>
        [HttpPost("obter-pelo-rosto")]
        public async Task<Models.Resposta<Domain.Models.Pessoa>> ObterPeloRosto([FromBody]Models.ObterPeloRosto viewModel)
        {
            if (!ModelState.IsValid)
                return ModelState.ObterResposta<Domain.Models.Pessoa>();

            var pessoa = await this._pessoas.ObterPelaFotoDoRosto(viewModel.FotoDoRostoEmBase64);
            return new Models.Resposta<Domain.Models.Pessoa>(pessoa);
        }

        /// <summary>
        /// Inserir uma pessoa
        /// </summary>
        /// <returns>Pessoa</returns>
        [HttpPost]
        public async Task<Models.Resposta<Domain.Models.Pessoa>> Post([FromBody]Models.CriarPessoa viewModel)
        {
            if (!ModelState.IsValid)
                return ModelState.ObterResposta<Domain.Models.Pessoa>();

            var pessoa = await this._pessoas.Inserir(viewModel.Nome, viewModel.FotosDoRostoEmBase64);
            return new Models.Resposta<Domain.Models.Pessoa>(pessoa);
        }
    }
}
