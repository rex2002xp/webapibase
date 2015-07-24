using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using RexStudioIdentity.Services;
using RexStudioIdentity.Validators;

namespace RexStudioIdentity.Models
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

            // Utilizamos la validacion personalizada.
            appUserManager.UserValidator = new MyCustomUserValidator(appUserManager)
            {
                AllowOnlyAlphanumericUserNames = true,
                RequireUniqueEmail = true
            };

            // Configuramos la validacion para el password
            appUserManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };

            // Utilizamos la validacion personalizada para la contraseña.
            appUserManager.PasswordValidator = new MyCustomPasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = true,
                RequireUppercase = true
            };

            appUserManager.EmailService = new EmailService();

            var dataProtectionProvider = options.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                appUserManager.UserTokenProvider =
                    new DataProtectorTokenProvider<ApplicationUser>(dataProtectionProvider.Create("ASP.NET Identity"))
                    {
                        TokenLifespan = TimeSpan.FromHours(6) // tiempo de vigencia del token generado que sera enviado por correo electronico.
                    };
            }

            return appUserManager;
        }
    }
}