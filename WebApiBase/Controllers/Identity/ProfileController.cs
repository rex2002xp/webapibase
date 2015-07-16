using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiBase.Http.Request.Identity;
using WebApiBase.Models.Identity;

namespace WebApiBase.Controllers.Identity
{
    /// <summary>
    /// Administracion del Perfil del usuario logueado
    /// </summary>
    [Authorize]
    [RoutePrefix("api/profile")]
    public class ProfileController : IdentityApiController
    {
        /// <summary>
        /// Retorna la informacion del usuario
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        public async Task<IHttpActionResult> get()
        {
            var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);
            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }
            return NotFound();
        }

        /// <summary>
        /// Actualiza la informacion del usuario
        /// </summary>
        /// <param name="request">Mensaje con la informacion del usuario</param>
        /// <returns></returns>
        [Route("")]
        [HttpPut]
        public async Task<IHttpActionResult> put(UserRequest request)
        {
            if (User.Identity.Name != request.Username)
            {
                return BadRequest();
            }

            var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);

            if (user == null)
            {
                return NotFound();
            }

            user.Email = request.Email;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Level = request.Level;
            user.PhoneNumber = request.PhoneNumber;

            await this.AppUserManager.UpdateAsync(user);
            return Ok(this.TheModelFactory.Create(user));
        }

        [Route("changepassword")]
        [HttpPost]
        public async Task<IHttpActionResult> post(ChangePasswordRequest request)
        {
            var user = await this.AppUserManager.FindByNameAsync(User.Identity.Name);
            await this.AppUserManager.ChangePasswordAsync(user.Id, request.OldPassword, request.Password);
            return Ok(Request.CreateResponse(HttpStatusCode.NoContent));
        }
    }
}
