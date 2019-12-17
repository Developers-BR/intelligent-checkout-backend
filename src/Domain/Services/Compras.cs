using System;
using System.Threading.Tasks;

namespace IntelligentCheckout.Backend.Domain.Services
{
    public interface ICompras
    {
        Task<Models.Compra> Finalizar(Guid idSessao, string fotoDoRostoEmBase64);
    }

    internal class Compras : ICompras
    {
        private readonly Repositories.ICarrinhosLeitura _carrinhoLeitura;
        private readonly Repositories.ICarrinhosEscrita _carrinhoEscrita;
        private readonly Repositories.IComprasEscrita _comprasEscrita;
        private readonly IPessoas _pessoas;
        private readonly IBlockchain _blockchain;

        public Compras(Repositories.ICarrinhosLeitura carrinhoLeitura,
                       Repositories.ICarrinhosEscrita carrinhoEscrita,
                       Repositories.IComprasEscrita comprasEscrita, 
                       IPessoas pessoas,
                       IBlockchain blockchain)
        {
            this._carrinhoLeitura = carrinhoLeitura;
            this._carrinhoEscrita = carrinhoEscrita;
            this._comprasEscrita = comprasEscrita;
            this._pessoas = pessoas;
            this._blockchain = blockchain;
        }

        public async Task<Models.Compra> Finalizar(Guid idSessao, string fotoDoRostoEmBase64)
        {
            var pessoa = await this._pessoas.ObterPelaFotoDoRosto(fotoDoRostoEmBase64);
            var carrinho = await this._carrinhoLeitura.ObterPorIdSessao(idSessao);

            var compra = new Models.Compra(pessoa, carrinho);
            await this._comprasEscrita.Inserir(compra);
            await this._blockchain.InserirContrato(compra);
            await this._carrinhoEscrita.ExcluirPorIdSessao(idSessao);
            return compra;
        }
    }
}
