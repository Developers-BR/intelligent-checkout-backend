using System;
using System.ComponentModel.DataAnnotations;

namespace IntelligentCheckout.Backend.Models
{
    /// <summary>
    /// Item de Compra
    /// </summary>
    public class ItemDeCompra
    {
        /// <summary>
        /// Id do Produto
        /// </summary>
        [Required(ErrorMessage = "Informe o produto")]
        public Guid IdProduto { get; set; }

        /// <summary>
        /// Quantidade
        /// </summary>
        [Range(1, 10, ErrorMessage = "Informe uma quantidade válida")]
        public int Quantidade { get; set; }
    }
}
