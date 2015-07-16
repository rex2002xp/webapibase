using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WebApiBase.Interfaces;

namespace WebApiBase.Http.Request.Identity
{
    public class CreateUserRequest : IUserIdentity
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
        /// Numero Telefonico.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Level
        /// </summary>
        public byte Level { get; set; }

        /// <summary>
        /// Rol
        /// </summary>
        [Display(Name = "Role")]
        public string RoleName { get; set; }
    }
}