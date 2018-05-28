using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel.Client;
using KSH.Okta.ApiClient.OpenId;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OpenIdConnect;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace KSH.Okta.ApiClient.OpenId
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConnectOkta(app);
        }

        /// <summary>
        ///     Configure OWIN to use OpenID Connect to log in with Okta.
        /// </summary>
        /// <param name="app"></param>
        private static void ConnectOkta(IAppBuilder app)
        {
            // These values are stored in Web.config. Make sure you update them!
            var clientId = ConfigurationManager.AppSettings["okta:ClientId"];
            var redirectUri = ConfigurationManager.AppSettings["okta:RedirectUri"];
            var authority = ConfigurationManager.AppSettings["okta:OrgUri"];
            var clientSecret = ConfigurationManager.AppSettings["okta:ClientSecret"];
            var postLogoutRedirectUri = ConfigurationManager.AppSettings["okta:PostLogoutRedirectUri"];
            
            app.SetDefaultSignInAsAuthenticationType(CookieAuthenticationDefaults.AuthenticationType);

            app.UseCookieAuthentication(new CookieAuthenticationOptions());

            app.UseOpenIdConnectAuthentication(new OpenIdConnectAuthenticationOptions
            {
                ClientId = clientId,
                ClientSecret = clientSecret,
                Authority = authority,
                RedirectUri = redirectUri,
                ResponseType = OpenIdConnectResponseType.CodeIdToken,
                Scope = OpenIdConnectScope.OpenIdProfile,
                PostLogoutRedirectUri = postLogoutRedirectUri,
                TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name"
                },

                Notifications = new OpenIdConnectAuthenticationNotifications
                {
                    AuthorizationCodeReceived = async n =>
                    {
                        // Exchange code for access and ID tokens
                        var tokenClient = new TokenClient(authority + "/v1/token", clientId, clientSecret);
                        var tokenResponse = await tokenClient.RequestAuthorizationCodeAsync(n.Code, redirectUri);

                        if (tokenResponse.IsError) throw new Exception(tokenResponse.Error);

                        var userInfoClient = new UserInfoClient(authority + "/v1/userinfo");
                        var userInfoResponse = await userInfoClient.GetAsync(tokenResponse.AccessToken);
                        var claims = new List<Claim>();
                        claims.AddRange(userInfoResponse.Claims);
                        claims.Add(new Claim("id_token", tokenResponse.IdentityToken));
                        claims.Add(new Claim("access_token", tokenResponse.AccessToken));

                        if (!string.IsNullOrEmpty(tokenResponse.RefreshToken)) claims.Add(new Claim("refresh_token", tokenResponse.RefreshToken));

                        n.AuthenticationTicket.Identity.AddClaims(claims);
                    },

                    RedirectToIdentityProvider = n =>
                    {
                        // If signing out, add the id_token_hint
                        if (n.ProtocolMessage.RequestType == OpenIdConnectRequestType.Logout)
                        {
                            var idTokenClaim = n.OwinContext.Authentication.User.FindFirst("id_token");

                            if (idTokenClaim != null) n.ProtocolMessage.IdTokenHint = idTokenClaim.Value;
                        }

                        return Task.CompletedTask;
                    }
                }
            });
        }
    }
}