using System;
using System.Collections.Generic;

namespace IntelligentCheckout.Backend.Domain.Models
{
    /// <summary>
    /// Produto
    /// </summary>
    public class Produto
    {
        /// <summary>
        /// Id do Produto
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Nome do Produto
        /// </summary>
        public string Nome { get; set; }
        /// <summary>
        /// Descrição do Produto
        /// </summary>
        public string Descricao { get; set; }
        /// <summary>
        /// Preço do Produto
        /// </summary>
        public decimal Preco { get; set; }

        /// <summary>
        /// Fotos do Produto
        /// </summary>
        public IList<string> Fotos { get; set; }

        /// <summary>
        /// Produto
        /// </summary>
        public Produto()
        {
            this.Id = Guid.NewGuid();
            this.Fotos = new List<string>();
        }
    }
}