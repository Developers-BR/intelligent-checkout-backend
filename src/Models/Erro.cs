namespace IntelligentCheckout.Backend.Models
{
    /// <summary>
    /// Erro
    /// </summary>
    public class Erro
    {
        /// <summary>
        /// Número
        /// </summary>
        public int Numero { get; set; }

        /// <summary>
        /// Mensagem
        /// </summary>
        public string Mensagem { get; set; }

        /// <summary>
        /// Cria uma mensagem de erro
        /// </summary>
        /// <param name="numero">Número</param>
        /// <param name="mensagem">Mensagem</param>
        public Erro(int numero, string mensagem)
        {
            this.Numero = numero;
            this.Mensagem = mensagem;
        }
    }
}
