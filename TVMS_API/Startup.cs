using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Owin;
using Owin;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using System.Web.Http;
using TVMS_API;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;

[assembly: OwinStartup(typeof(TVMS_API.Startup))]

namespace TVMS_API
{
    public partial class Startup
    {
        static Startup()
        {
           // PublicClientId = "self";

           // UserManagerFactory = () => new UserManager<IdentityUser>(new UserStore<IdentityUser>());
        }
        public void Configuration(IAppBuilder app)
        {

            HttpConfiguration config = new HttpConfiguration();
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            var myProvider = new MyAuthorizationServerProvider();

          

            OAuthAuthorizationServerOptions option = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(15),
                Provider = myProvider
            };
           /// app.UseOAuthAuthorizationServer(options);
           // app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
           // HttpConfiguration config = new HttpConfiguration();
           // WebApiConfig.Register(config);

            // Token Generation
            
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
            app.UseOAuthAuthorizationServer(option);
            WebApiConfig.Register(config);
            
        }

        
    }
}