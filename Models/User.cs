using Microsoft.AspNetCore.Identity;

namespace stock_portfolio_server.Models 
{
    public class User : IdentityUser
    {
        public string Token { get; set; }
    }
}