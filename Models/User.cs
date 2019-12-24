using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace stock_portfolio_server.Models 
{
    public class User : IdentityUser
    {
        public string Token { get; set; }
        public virtual List<ExternalAccount> externalAccounts { get; set; }
        public virtual List<Stock> userStocks { get; set; }
    }
}