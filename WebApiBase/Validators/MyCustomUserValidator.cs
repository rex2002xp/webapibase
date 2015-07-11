using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WebApiBase.Models.Identity;

namespace WebApiBase.Validators
{
    /// <summary>
    /// Personalizacion de las validaciones que debe cumplir el usuario.
    /// </summary>
    public class MyCustomUserValidator : UserValidator<ApplicationUser>
    {
        List<string> _allowedEmailDomains = new List<string> { "outlook.com", "hotmail.com", "gmail.com", "yahoo.com" };

        /// <summary>
        /// Validamos que el correo electronico asociado al usuario debe pertener unicamente a los dominios outlook.com, hotmail.com, gmail.com y yahoo.com
        /// </summary>
        /// <param name="appUserManager"></param>
        public MyCustomUserValidator(ApplicationUserManager appUserManager)
            : base(appUserManager)
        {
        }

        public override async Task<IdentityResult> ValidateAsync(ApplicationUser user)
        {
            IdentityResult result = await base.ValidateAsync(user);
            var emailDomain = user.Email.Split('@')[1];
            if (!_allowedEmailDomains.Contains(emailDomain.ToLower()))            {
                var errors = result.Errors.ToList();
                errors.Add(String.Format("Email domain '{0}' is not allowed", emailDomain));
                result = new IdentityResult(errors);
            }
            return result;
        }
    }
}