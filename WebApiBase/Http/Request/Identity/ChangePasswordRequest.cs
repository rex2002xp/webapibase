using WebApiBase.Interfaces;

namespace WebApiBase.Http.Request.Identity
{
    public class ChangePasswordRequest : IPasswordChangeIdentity
    {
        /// <summary>
        /// Password actual
        /// </summary>
        public string OldPassword { get ; set ; }

        /// <summary>
        /// Password nuevo
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Confirmacion de password
        /// </summary>
        public string ConfirmPassword { get; set; }
    }
}