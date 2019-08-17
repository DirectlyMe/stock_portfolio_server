namespace stock_portfolio_server.Models
{
    public class RobinhoodUser
    {
        public string userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string mfa { get; set; }
    }
}