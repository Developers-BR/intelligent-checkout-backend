namespace IntelligentCheckout.Backend.Models
{
    /// <summary>
    /// Aviso
    /// </summary>
    public class Aviso
    {
        /// <summary>
        /// Propriedade
        /// </summary>
        public string Propriedade { get; set; }

        /// <summary>
        /// Mensagem
        /// </summary>
        public string Mensagem { get; set; }

        public Aviso(string propriedade, string mensagem)
        {
            this.Propriedade = propriedade;
            this.Mensagem = mensagem;
        }
    }
}
