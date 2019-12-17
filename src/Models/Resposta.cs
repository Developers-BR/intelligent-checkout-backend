using System.Collections.Generic;

namespace IntelligentCheckout.Backend.Models
{
    /// <summary>
    /// Resposta de API
    /// </summary>
    public class Resposta
    {
        public static Resposta OK = new Resposta();

        /// <summary>
        /// Avisos
        /// </summary>
        public IList<Aviso> Avisos { get; } = new List<Aviso>();

        /// <summary>
        /// Erro
        /// </summary>
        public Erro Erro { get; set; }

        public Resposta() { }

        public Resposta(Erro erro)
            => this.Erro = erro;
    }

    /// <summary>
    /// Resposta de API
    /// </summary>
    /// <typeparam name="T">Entidade</typeparam>
    public class Resposta<T> : Resposta
    {
        /// <summary>
        /// Data
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Cria uma nova resposta
        /// </summary>
        /// <param name="data"></param>
        public Resposta(T data)
            => this.Data = data;
    }
}
