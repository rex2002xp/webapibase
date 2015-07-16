using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiBase.Http.Response.Identity
{
    /// <summary>
    /// Respuesta con la informacion del usuario
    /// </summary>
    public class UserResponse
    {
        /// <summary>
        /// Url con el que se puede consultar la informacion del usuario
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Identificador unico.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Username unico.
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Nombre Completo.
        /// </summary>
        public string FullName { get; set; }
        /// <summary>
        /// Correo Electronico.
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Bool que indica si el usuario a confirmado o no su correo electronico.
        /// </summary>
        public bool EmailConfirmed { get; set; }
        /// <summary>
        /// Nivel jerarquico.
        /// </summary>
        public int Level { get; set; }
        /// <summary>
        /// Fecha de Registro.
        /// </summary>
        public DateTime JoinDate { get; set; }
        /// <summary>
        /// Roles asociados.
        /// </summary>
        public IList<string> Roles { get; set; }
        /// <summary>
        /// Notificaciones asociadas.
        /// </summary>
        public IList<System.Security.Claims.Claim> Claims { get; set; }
    }
}