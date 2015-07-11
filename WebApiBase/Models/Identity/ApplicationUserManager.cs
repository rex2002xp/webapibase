using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;

namespace WebApiBase.Models.Identity
{
    /// <summary>
    /// Expone el api relacionado con los usuarios para guardar automatiacamente los cambios en el repositorio de Usuarios (UserStore).
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }

        /// <summary>
        /// Permite generar un objeto de tipo ApplicationUserManager utilizando el contexto Owin que recibe como parametro.
        /// </summary>
        /// <param name="options">Opciones de configuracion para el IdentityFactoryMiddleware</param>
        /// <param name="context">Diccionario Owin que envuelve todos los objetos fuertemente tipados definidos en el mapa de tablas 
        /// y sus entidades relacionadas.</param>
        /// <returns></returns>
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context)
        {
            var appDbContext = context.Get<ApplicationDbContext>();
            var appUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(appDbContext));

            return appUserManager;
        }
    }    
}