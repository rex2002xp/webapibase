using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Newtonsoft.Json.Serialization;
using Owin;
using RexStudioIdentity;
using RexStudioIdentity.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using WebApiBase.Providers;

[assembly: OwinStartup(typeof(WebApiBase.Startup))]

namespace WebApiBase
{
    /// <summary>
    /// Clase con la que se inicializa la aplicacion
    /// </summary>
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();
            ConfigureOAuthTokenGeneration(app);
            ConfigureWebApi(httpConfig);
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            app.UseWebApi(httpConfig);
        }

        /// <summary>
        /// Configuracion del middleware para usar Oauth con Token Bearer.
        /// </summary>
        /// <param name="app"></param>
        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            // Se configura el contexto de la base de datos y la administracion del usuario, 
            // para que sea usada una unica instancia por peticion.
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create);

            string issueServer = ConfigurationManager.AppSettings["as:IssueServer"];

            // Configuracion del middleware OAuth Bearer 
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/oauth/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat(issueServer)
            };

            app.UseOAuthAuthorizationServer(OAuthServerOptions);
        }

        /// <summary>
        /// Permite configurar el proceso de validacion para todos los controladores de tipo API que esten marcados con [Authorize].
        /// </summary>
        /// <param name="app">Aplicacion</param>
        private void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {

            string issueServer = ConfigurationManager.AppSettings["as:IssueServer"];
            string audienceId = ConfigurationManager.AppSettings["as:AudienceId"];
            byte[] audienceSecret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audienceId },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issueServer, audienceSecret)
                    }
                });
        }

        /// <summary>
        /// Permite configurar las caracteristicas del servicio Api
        /// </summary>
        /// <param name="config"></param>
        private void ConfigureWebApi(HttpConfiguration config)
        {
            config.MapHttpAttributeRoutes();

            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}
