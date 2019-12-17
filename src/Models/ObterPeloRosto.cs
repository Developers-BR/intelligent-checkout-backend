using System.ComponentModel.DataAnnotations;

namespace IntelligentCheckout.Backend.Models
{
    /// <summary>
    /// Obter pelo Rosto
    /// </summary>
    public class ObterPeloRosto
    {
        /// <summary>
        /// Foto do Rosto em Base64
        /// </summary>
        [Required(ErrorMessage = "Informe uma foto do Rosto")]
        public string FotoDoRostoEmBase64 { get; set; }
    }
}
