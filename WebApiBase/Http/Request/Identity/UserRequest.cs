using System.ComponentModel.DataAnnotations;

namespace WebApiBase.Http.Request.Identity
{
    public class UserRequest
    {
        /// <summary>
        /// Correo Electronico
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Correo Electronico")]
        public string Email { get; set; }

        /// <summary>
        /// Usuario
        /// </summary>
        [Required]
        [Display(Name = "Usuario")]
        public string Username { get; set; }

        /// <summary>
        /// Nombres
        /// </summary>
        [Required]
        [Display(Name = "Nombres")]
        public string FirstName { get; set; }

        /// <summary>
        /// Apellidos
        /// </summary>
        [Required]
        [Display(Name = "Apellidos")]
        public string LastName { get; set; }

        /// <summary>
        /// Rol
        /// </summary>
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "El {0} debe tener {2} caracteres de largo.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Confirmacion del password
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmacion de password")]
        [Compare("Password", ErrorMessage = "El password y la confirmacion no son iguales.")]
        public string ConfirmPassword { get; set; }
    }
}