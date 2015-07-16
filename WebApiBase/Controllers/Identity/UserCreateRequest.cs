using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApiBase.Interfaces;

namespace WebApiBase.Controllers.Identity
{
    public class UserCreateRequest : IUserIdentity , IPasswordIdentity
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
        /// Numero Telefonico.
        /// </summary>
        [Display(Name = "Telefono")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        [Display(Name = "Password")]
        public string Password { get ; set; }

        /// <summary>
        /// Confirmacion de Password
        /// </summary>
        [Display(Name = "Confirmacion de Password")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Nivel de acceso
        /// </summary>
        public byte Level { get; set; }
    }
}