using System.ComponentModel.DataAnnotations;

namespace stock_portfolio_server.ViewModels
{
    public class ExternalAccountView
    {
        public string type;
        public string username;
        
        [DataType(DataType.Password)]
        public string password;
    }
}