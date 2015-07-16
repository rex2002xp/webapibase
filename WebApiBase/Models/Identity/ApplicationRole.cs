using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace WebApiBase.Models.Identity
{
    /// <summary>
    /// Representa la entidad rol, se hereda de IdentityRole para poder tener los mismo atributos, pero a la vez podemos personalizar
    /// la entidad.
    /// </summary>
    public class ApplicationRole : IdentityRole
    {
        /// <summary>
        /// Descripcion en pocas palabras del rol y su uso.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Description { set; get; }
    }
}