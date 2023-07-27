using IdentityModel.OidcClient.Browser;
using IdentityModel.OidcClient;
using Microsoft.AspNetCore.Mvc;

namespace invest.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController
    {
        public AuthController()
        {
            var options = new OidcClientOptions
            {
                Authority = _authority,
                ClientId = "interactive.public",
                RedirectUri = redirectUri,
                Scope = "openid profile api",
                FilterClaims = false,
                Browser = browser,
            };

        }

        public void Login()
        {

        }
    }
}
