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
    /// Servicio para obtener informacion sobre los usuarios.
    /// </summary>
    [RoutePrefix("api/user")]
    public class UserController : IdentityApiController
    {
        /// <summary>
        /// Retorna todos los usuarios.
        /// </summary>
        /// <returns></returns>
        [Route("")]
        public IHttpActionResult GetUsers()
        {
            return Ok(this.AppUserManager.Users.ToList().Select(u => this.TheModelFactory.Create(u)));
        }

        /// <summary>
        /// Busca un usuario en base al id.
        /// </summary>
        /// <param name="Id">Identificador unico del usuario</param>
        /// <returns>UserResponse</returns>
        [Route("{id:guid}", Name = "GetUserById")]
        public async Task<IHttpActionResult> GetUser(string Id)
        {
            var user = await this.AppUserManager.FindByIdAsync(Id);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        /// <summary>
        /// Busca un usuario en base al nombre
        /// </summary>
        /// <param name="username"></param>
        /// <returns>UserResponse</returns>
        [Route("user/{username}")]
        public async Task<IHttpActionResult> GetUserByName(string username)
        {
            var user = await this.AppUserManager.FindByNameAsync(username);

            if (user != null)
            {
                return Ok(this.TheModelFactory.Create(user));
            }

            return NotFound();

        }

        /// <summary>
        /// Permite crear un usuario.
        /// </summary>
        /// <param name="UserRequest">Peticion con la informacion del usuario que se desea crear.</param>
        /// <returns></returns>
        [Route("create")]
        public async Task<IHttpActionResult> CreateUser(UserRequest UserRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new ApplicationUser()
            {
                UserName = UserRequest.Username,
                Email = UserRequest.Email,
                FirstName = UserRequest.FirstName,
                LastName = UserRequest.LastName,
                Level = 3,
                JoinDate = DateTime.Now.Date,
            };

            IdentityResult addUserResult = await this.AppUserManager.CreateAsync(user, UserRequest.Password);

            if (!addUserResult.Succeeded)
            {
                return GetErrorResult(addUserResult);
            }

            // Generacion del codigo para confirmacion.
            string code = await this.AppUserManager.GenerateEmailConfirmationTokenAsync(user.Id);

            // Generacion del Link para confirmacion.
            var callbackUrl = new Uri(
                Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = code })
                );
            try
            {
                // Envio de correo al usuario.
                await this.AppUserManager.SendEmailAsync(user.Id, "WebApi Rest Confirmacion", "Hemos recibido tu solicitud. Por favor confirma tu cuenta haciendo clic <a href=\"" + callbackUrl + "\">AQUI</a>");

                Uri locationHeader = new Uri(Url.Link("GetUserById", new { id = user.Id }));

                return Created(locationHeader, TheModelFactory.Create(user));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        /// <summary>
        /// Permite confirmar la cuenta de correo asociada al usuario.
        /// </summary>
        /// <param name="userId">Identificador unico del usuario</param>
        /// <param name="code">Codigo de confirmacion</param>
        /// <returns></returns>
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(string userId = "", string code = "")
        {
            if (string.IsNullOrWhiteSpace(userId) || string.IsNullOrWhiteSpace(code))
            {
                ModelState.AddModelError("", "User Id and Code are required");
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ConfirmEmailAsync(userId, code);

            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return GetErrorResult(result);
            }
        }

        /// <summary>
        /// Cambio de Contraseña
        /// </summary>
        /// <param name="ChangePasswordRequest">Peticion con la informacion del usuario para realizar el cambio de contraseña.</param>
        /// <returns></returns>
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordRequest ChangePasswordRequest)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IdentityResult result = await this.AppUserManager.ChangePasswordAsync(User.Identity.GetUserId(), ChangePasswordRequest.OldPassword, ChangePasswordRequest.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        /// <summary>
        /// Permite eliminar un usuario.
        /// </summary>
        /// <param name="id">Identificador unico del usuario a eliminar.</param>
        /// <returns></returns>
        [Route("user/{id:guid}")]
        public async Task<IHttpActionResult> DeleteUser(string id)
        {

            // Seria una excelente idea, que esta opcion solo este disponible para los usuarios Super 
            // Administradores o Administradores , esto lo implementaremos mas adelante.
            var appUser = await this.AppUserManager.FindByIdAsync(id);
            if (appUser != null)
            {
                IdentityResult result = await this.AppUserManager.DeleteAsync(appUser);
                if (!result.Succeeded)
                {
                    return GetErrorResult(result);
                }
                return Ok();
            }

            return NotFound();
        }

        /// <summary>
        /// Permite asignarle roles a un usuario en especifico.
        /// </summary>
        /// <param name="id">Identificador unico del usuario</param>
        /// <param name="rolesToAssign">Array con el listado de roles.</param>
        /// <returns></returns>
        [Route("user/{id:guid}/roles")]
        [HttpPut]
        public async Task<IHttpActionResult> AssignRolesToUser([FromUri] string id, [FromBody] string[] rolesToAssign)
        {
            var appUser = await this.AppUserManager.FindByIdAsync(id);
            if (appUser == null)
            {
                return NotFound();
            }

            var currentRoles = await this.AppUserManager.GetRolesAsync(appUser.Id);
            var rolesNotExists = rolesToAssign.Except(this.AppRoleManager.Roles.Select(x => x.Name)).ToArray();

            if (rolesNotExists.Count() > 0)
            {
                ModelState.AddModelError("", string.Format("Roles '{0}' does not exixts in the system", string.Join(",", rolesNotExists)));
                return BadRequest(ModelState);
            }

            IdentityResult removeResult = await this.AppUserManager.RemoveFromRolesAsync(appUser.Id, currentRoles.ToArray());

            if (!removeResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to remove user roles");
                return BadRequest(ModelState);
            }

            IdentityResult addResult = await this.AppUserManager.AddToRolesAsync(appUser.Id, rolesToAssign);

            if (!addResult.Succeeded)
            {
                ModelState.AddModelError("", "Failed to add user roles");
                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
