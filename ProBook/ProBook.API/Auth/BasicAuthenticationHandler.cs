using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace ProBook.API.Auth
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                return AuthenticateResult.Fail("Unauthorized");

            }
            string authorizationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authorizationHeader))
                return AuthenticateResult.Fail("Unauthorized");
            if(!authorizationHeader.StartsWith("basic ",StringComparison.OrdinalIgnoreCase))
                return AuthenticateResult.Fail("Unauthorized");

            var token = authorizationHeader.Substring(6);

            var credentialAsString=Encoding.UTF8.GetString(Convert.FromBase64String(token));

            var credential = credentialAsString.Split(":");
            if (credential?.Length != 2) {
                return AuthenticateResult.Fail("Unauthorized");

            }
            var username=credential[0];
            var password=credential[1];

            
            var claims = new[]
       {
            new Claim(ClaimTypes.NameIdentifier, username)
        };
            var identity = new ClaimsIdentity(claims, "Basic");
            var claimsPrincipal = new ClaimsPrincipal(identity);
            return AuthenticateResult.Success(new AuthenticationTicket(claimsPrincipal, Scheme.Name));
        }
    }
}
