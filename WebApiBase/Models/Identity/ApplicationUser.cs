using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebApiBase.Models.Identity
{
    /// <summary>
    /// Representa la entidad Usuario, se hereda de IdentityUser para poder tener los mismo atributos, pero a la vez podemos personalizar la entidad
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Nombres del usuario.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        /// <summary>
        /// Apellidos del usuario.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        /// <summary>
        /// Nivel jerarquico
        /// </summary>
        [Required]
        public byte Level { get; set; }

        /// <summary>
        /// Fecha de Registro
        /// </summary>
        [Required]
        public DateTime JoinDate { get; set; }
    }
}