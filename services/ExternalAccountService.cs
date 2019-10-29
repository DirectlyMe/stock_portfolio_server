using System;
using System.Linq;
using System.Threading.Tasks;
using stock_portfolio_server.Models;

namespace stock_portfolio_server.services
{
    public interface IExternalAccountService
    {
        Task<AuthResponse> Authorize(string userId, int accountId, int mfaCode);
        Task<AuthResponse> Authorize(string userId, int accountId);
        Task<AuthResponse> Authorize(ExternalAccount userAccount);
        Task<AuthResponse> Authorize(ExternalAccount userAccount, int mfaCode);
        void GetAccounts(string token);
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

        public async Task<AuthResponse> Authorize(string userId, int accountId)
        {
            var userAccount = _userDbContext.ExternalAccount.Where(account => account.userId == userId && account.type.typeId == accountId)
                              .First();

            switch (userAccount.accountId)
            {
                case 1: // robinhood
                    return await _robinhoodService.Authorize(userAccount);
                default: 
                    throw new Exception($"User doesn't have a account of type: {accountId}");
            }
        }

        public Task<AuthResponse> Authorize(string userId, int accountId, int mfaCode)
        {
            var userAccount = _userDbContext.ExternalAccount.Where(account => account.userId == userId && account.type.typeId == accountId)
                              .First();

            switch (userAccount.accountId)
            {
                case 1: // robinhood
                    return _robinhoodService.Authorize(userAccount, mfaCode);
                default: 
                    throw new Exception($"User doesn't have a account of type: {accountId}");
            }
        }

        public Task<AuthResponse> Authorize(ExternalAccount userAccount)
        {
            throw new NotImplementedException();
        }

        public Task<AuthResponse> Authorize(ExternalAccount userAccount, int mfaCode)
        {
            throw new NotImplementedException();
        }

        public void GetAccounts(string token)
        {
            throw new System.NotImplementedException();
        }
    }
}