using System;
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

namespace stock_portfolio_server.services
{
    public class RobinhoodAccountService : IExternalAccountService
    {
        public const int TYPE_ID = 1;
        private string baseUrl = "https://api.robinhood.com/";
        private string loginUrl = "/oauth2/token/";
        private string accountsUrl = "/accounts/";
        private readonly IHttpClientFactory _clientFactory;
        public UserDbContext _userContext;

        public RobinhoodAccountService(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<AuthResponse> Authorize(ExternalAccount userAccount) 
        {            
            var payload = new Dictionary<string, string>
            {
                { "username", userAccount.username },
                { "password", userAccount.password },
                { "client_id", "c82SH0WZOsabOXGP2sxqcj34FxkvfnWRZBKlBjFS" },
                { "device_token", "d13a1d4a-88cc-4db4-8b76-b7301626f70a"},
                { "grant_type", "password" },
                // { "mfa_code", user.mfa }
            };

            var authorizedUser = await AuthorizeUser(payload);
            if (authorizedUser.access_token == null)
                throw new Exception("Authorization failed");

            return authorizedUser;
        }

        public async void GetAccounts(string token)
        {
            var client = _clientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Get, $"{baseUrl}{accountsUrl}");
            request.Headers.Add("Authorization", $"Bearer {token}");

            var response = await client.SendAsync(request);
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

