using IntelligentCheckout.Backend.Infra;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IntelligentCheckout.Backend.Models
{
    /// <summary>
    /// Criar Pessoa
    /// </summary>
    public class CriarPessoa
    {
        /// <summary>
        /// Nome da Pessoa
        /// </summary>
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Nome deve conter entre 5 e 50 caracteres")]
        public string Nome { get; set; }

        /// <summary>
        /// Foto do Rosto em Base64
        /// </summary>
        [EnsureMinimumElements(3, ErrorMessage = "Informe pelo menos três fotos do Rosto")]
        public List<string> FotosDoRostoEmBase64 { get; set; }
    }
}
