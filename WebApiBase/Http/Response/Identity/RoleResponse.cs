using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;

namespace WebApiBase.Http.Response.Identity
{
    /// <summary>
    /// Respuesta con la informacion del Rol
    /// </summary>
    public class RoleResponse
    {
        /// <summary>
        /// Url para consultar la informacion del rol.
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// Identificador unico.
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// Nombre del Rol.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Descripcion corta del rol.
        /// </summary>
        public string Description { get; set; }
    }
}