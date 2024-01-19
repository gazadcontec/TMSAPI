using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.Owin.Security.OAuth;

namespace TVMS_API
{
    public class MyAuthorizationServerProvider:OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
           // return base.ValidateClientAuthentication(context);
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            //return base.GrantResourceOwnerCredentials(context);

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            if(context.UserName=="admin" && context.Password=="admin")
            {
                //identity.AddClaim(new Claim(ClaimTypes.Role,"admin"));
                //identity.AddClaim(new Claim("username", "admin"));
                //identity.AddClaim(new Claim(ClaimTypes.Name,"Saurabh"));
                //context.Validated(identity);
            }

            else if (context.UserName == "user" && context.Password == "user")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim("username", "user"));
                identity.AddClaim(new Claim(ClaimTypes.Name, "Bansal"));
                context.Validated(identity);
            }

            else
            {
                context.SetError("invalid_grant", "Provided username & password is incorrect.");
                return;
            }
        }
    }
}