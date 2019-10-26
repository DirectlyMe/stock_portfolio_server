using System;
using System.Linq;
using System.Threading.Tasks;
using stock_portfolio_server.Models;

namespace stock_portfolio_server.services
{
    public interface IExternalAccountService
    {
        public Task<AuthResponse> Authorize(ExternalAccount userAccount);
        public void GetAccounts(string token);
    }

    public class ExternalAccountService : IExternalAccountService
    {
        private readonly RobinhoodAccountService _robinhoodService;
        private readonly UserDbContext _userDbContext;

        public ExternalAccountService(RobinhoodAccountService robinhoodService, UserDbContext userDbContext)
        {
            _robinhoodService = robinhoodService;
            _userDbContext = userDbContext;
        }

        public Task<AuthResponse> Authorize(string userId, string username, string password, string accountTypeName)
        {
            var userAccount = _userDbContext.ExternalAccount.Where(account => account.userId == userId && account.type.name == accountTypeName).First();

            switch (userAccount.type.name)
            {
                case "robinhood":
                    return _robinhoodService.Authorize(userAccount);
                default: 
                    throw new Exception($"User doesn't have a account of type: {accountTypeName}");
            }
        }

        public void GetAccounts(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}