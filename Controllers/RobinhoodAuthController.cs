using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using stock_portfolio_server.Models;
using stock_portfolio_server.services;

namespace stock_portfolio_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class RobinhoodAuthController : Controller
    {
        private string baseUrl = "https://api.robinhood.com/";
        private string loginUrl = "/oauth2/token/";
        private readonly IHttpClientFactory _clientFactory;
        public UserDbContext _userContext;

        public RobinhoodAuthController(IHttpClientFactory clientFactory, UserDbContext userContext)
        {
            _clientFactory = clientFactory;
            _userContext = userContext;
        }

        [HttpGet]
        public IActionResult GetCredentials()
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;

            var robinhoodCredentials = _userContext.Users.Select(x => x.externalAccounts.Where(p => p.userId == userId));
            return StatusCode(200);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] RobinhoodUser user)
        {
            var claimsIdentity = this.User.Identity as ClaimsIdentity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            
            // var user = _userContext.Users.

            var payload = new Dictionary<string, string>
            {
                { "username", user.username },
                { "password", user.password },
                { "client_id", "c82SH0WZOsabOXGP2sxqcj34FxkvfnWRZBKlBjFS" },
                { "device_token", "d13a1d4a-88cc-4db4-8b76-b7301626f70a"},
                { "grant_type", "password" },
                // { "mfa_code", user.mfa }
            };

            var authorizedUser = await AuthorizeUser(payload);
            if (authorizedUser.access_token == null)
                return BadRequest(new { error = "Invalid credentials" });

            return Json(authorizedUser);
        }

        private async Task<RobinhoodAuthResponse> AuthorizeUser(Dictionary<string, string> payload)
        {
            var client = _clientFactory.CreateClient();
            var content = new FormUrlEncodedContent(payload);
            var response = await client.PostAsync($"{baseUrl}/{loginUrl}", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var robinhoodResponse = JsonConvert.DeserializeObject<RobinhoodAuthResponse>(responseString);

            return robinhoodResponse;
        }
    }
}