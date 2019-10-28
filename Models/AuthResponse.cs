namespace stock_portfolio_server.Models
{
    public class AuthResponse
    {
        public string access_token { get; set; }
        public bool mfa_required { get; set; }
    }
}