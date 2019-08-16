namespace stock_portfolio_server.Models
{
    public class RobinhoodAuthResponse
    {
        public string access_token;
        public string expires_in;
        public string token_type;
        public string scope;
        public string refresh_token;
        public string mfa_code;
        public string backup_code;
    }
}