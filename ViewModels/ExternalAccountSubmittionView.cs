using System.ComponentModel.DataAnnotations;

namespace stock_portfolio_server.ViewModels
{
    public class ExternalAccountSubmittionView
    {
        public string type { get; set; }
        public string username { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        [Compare("password")]
        [DataType(DataType.Password)]
        public string confirmPassword { get; set; }
    }
}