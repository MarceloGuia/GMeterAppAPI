using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace MobWebApp.Helpers
{
    public class AppOAuthProvider : OAuthAuthorizationServerProvider
    {
        public readonly String _PublicClientID;

        public AppOAuthProvider(String publicClientID)
        {
            _PublicClientID = publicClientID;
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, context.UserName));
            ClaimsIdentity oAuthClaimIdentity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
            ClaimsIdentity cookiesClaimIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationType);
            AuthenticationProperties properties = CreateProperties(context.UserName);
            AuthenticationTicket ticket = new AuthenticationTicket(oAuthClaimIdentity, properties);
            context.Validated(ticket);
            context.Request.Context.Authentication.SignIn(cookiesClaimIdentity);
        }

        public static AuthenticationProperties CreateProperties(String UserName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                {
                    "UserName", UserName
                }
            };
            return new AuthenticationProperties(data);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            if (context.ClientId == null)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
        {
            Uri expectedRootUri = new Uri(context.Request.Uri, "/");
            if (expectedRootUri.AbsoluteUri == context.RedirectUri)
            {
                context.Validated();
            }
            return Task.FromResult<object>(null);
        }
    }
}