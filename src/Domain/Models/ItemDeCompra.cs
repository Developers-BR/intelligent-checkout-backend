namespace IntelligentCheckout.Backend.Domain.Models
{
    /// <summary>
    /// Item de Compra
    /// </summary>
    public class ItemDeCompra
    {
        /// <summary>
        /// Id do Produto
        /// </summary>
        public Produto Produto { get; set; }

        /// <summary>
        /// Quantidade
        /// </summary>
        public int Quantidade { get; set; }

        /// <summary>
        /// Item de Compra
        /// </summary>
        public ItemDeCompra() { }

        /// <summary>
        /// Item de compra
        /// </summary>
        /// <param name="produto">Produto</param>
        /// <param name="quantidade">Quantidade</param>
        public ItemDeCompra(Produto produto, int quantidade)
            : this()
        {
            this.Produto = produto;
            this.Quantidade = quantidade;
        }
    }
}
