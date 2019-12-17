using System;
using System.ComponentModel.DataAnnotations;
using System.IO;

namespace IntelligentCheckout.Backend.Models
{
    /// <summary>
    /// Compra
    /// </summary>
    public class FinalizarCompra
    {
        /// <summary>
        /// Id da Sessão
        /// </summary>
        [Required(ErrorMessage = "Id da Sessão inválido")]
        public Guid IdSessao { get; set; }

        /// <summary>
        /// Foto do Rosto
        /// </summary>
        [Required(ErrorMessage = "Informe uma foto do Rosto")]
        public string FotoDoRostoEmBase64 { get; set; }
    }
}
