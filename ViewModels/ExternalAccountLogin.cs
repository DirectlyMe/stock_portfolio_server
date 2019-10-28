using System.ComponentModel.DataAnnotations;

namespace stock_portfolio_server.ViewModels
{
    public class ExternalAccountLogin
    {
        public int AccountId { get; set; }
        public string UserName { get; set; }
    }
}