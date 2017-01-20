using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace RexStudioIdentity.Models
{
    /// <summary>
    /// Expone el api relacionado con los roles para guardar automatiacamente los cambios en el repositorio de roles (RoleStore).
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole, string> store) : base(store)
        {
        }

        /// <summary>
        /// Permite generar un objeto de tipo ApplicationRoleManager utilizando el contexto Owin que recibe como parametro.
        /// </summary>
        /// <param name="options">Opciones de configuracion para el IdentityFactoryMiddleware</param>
        /// <param name="context">Diccionario Owin que envuelve todos los objetos fuertemente tipados definidos en el mapa de tablas 
        /// y sus entidades relacionadas.</param>
        /// <returns></returns>
        public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<ApplicationDbContext>();
            var appRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context.Get<ApplicationDbContext>()));

            return appRoleManager;
        }
    }
}
