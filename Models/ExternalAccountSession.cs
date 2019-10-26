namespace stock_portfolio_server.Models
{
    // #nullable enable
    public interface IExternalAccountSession
    {
        void Authenticate(string username, string password);
        void Authenticate(string username, string password, string mfaCode);
        void GetAccounts();
    }

    public class ExternalAccountSession : IExternalAccountSession
    {
        public ExternalAccountSession(string username, string password, string mfaCode) {
            
        }

        public void Authenticate(string username, string password)
        {
            throw new System.NotImplementedException();
        }

        public void Authenticate(string username, string password, string mfaCode)
        {
            throw new System.NotImplementedException();
        }

        public void GetAccounts()
        {
            throw new System.NotImplementedException();
        }
    }
}