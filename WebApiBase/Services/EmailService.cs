using Microsoft.AspNet.Identity;
using System.Threading.Tasks;

namespace WebApiBase.Services
{
    /// <summary>
    /// Servicio que permite las notificaciones por correo electronico.
    /// </summary>
    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            return Task.FromResult(0);
        }
    }
}