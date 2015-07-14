using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiBase.Http.Request.Identity
{
    public class ChangePasswordRequest
    {
        /// <summary>
        /// Password actual.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        /// <summary>
        /// Password nuevo.
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener {2} caracteres minimo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        /// <summary>
        /// Confirmacion del password nuevo.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "El nuevo password y la confirmacion no coinciden.")]
        public string ConfirmPassword { get; set; }
    }
}