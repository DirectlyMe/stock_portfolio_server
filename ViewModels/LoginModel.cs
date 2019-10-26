using System.ComponentModel.DataAnnotations;

namespace stock_portfolio_server.ViewModels
{
    public class LoginModel
    {
        public string accountTypeName { get; set; }
        public string UserName { get; set; }
        
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}