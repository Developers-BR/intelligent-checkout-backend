using System;

namespace IntelligentCheckout.Backend.Domain.Models
{
    /// <summary>
    /// Compra
    /// </summary>
    public class Compra
    {
        /// <summary>
        /// Id da compra
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Data e hora da compra
        /// </summary>
        public DateTime DataHora { get; set; }

        /// <summary>
        /// Pessoa que efetuou a compra
        /// </summary>
        public Pessoa Pessoa { get; set; }

        /// <summary>
        /// Carrinho de compras
        /// </summary>
        public Carrinho Carrinho { get; set; }

        /// <summary>
        /// Cria uma compra
        /// </summary>
        public Compra()
        {
            this.Id = Guid.NewGuid();
            this.DataHora = DateTime.Now;
        }

        /// <summary>
        /// Cria uma compra
        /// </summary>
        public Compra(Pessoa pessoa, Carrinho carrinho) 
            : this()
        {
            this.Pessoa = pessoa;
            this.Carrinho = carrinho;

            this.Pessoa.DeixarApenasUmaFotosBase64();
        }
    }
}