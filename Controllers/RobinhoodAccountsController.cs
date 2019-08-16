using System.Collections;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using stock_portfolio_server.Models;

namespace stock_portfolio_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RobinhoodAccountsController
    {
        private string baseUrl = "https://api.robinhood.com/";
        private string accountsUrl = "/accounts/";
        private readonly IHttpClientFactory _clientFactory;

        public RobinhoodAccountsController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet]
        public async Task Get()
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, 
            $"{baseUrl}{accountsUrl}");
            // request.Headers.Add("Authorization", $"Bearer {ApplicationStore.robinhoodToken}");
            
            var response = await client.SendAsync(request);
        }
    }
}