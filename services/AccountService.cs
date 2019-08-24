using System.Collections;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using stock_portfolio_server.Models;

namespace stock_portfolio_server.services
{
    interface IAccountService 
    {
        Task<IEnumerable> GetAccounts(string userId);
        Task<ExternalAccount> CreateAccount(string username, string password, string type, string userId);
    }

    public class AccountService : IAccountService
    {
        private readonly UserDbContext _userContext;

        public AccountService(UserDbContext userContext)
        {
            _userContext = userContext;
        }

        public async Task<ExternalAccount> CreateAccount(string username, string password, string type, string userId)
        {
            try {
                var selectedType = await _userContext.AccountType.FindAsync(type);
                
                if (selectedType == null) return null;

                var newAccount = new ExternalAccount
                {
                    userId = userId,
                    type = selectedType, 
                    username = username,
                    password = password 
                };

                _userContext.ExternalAccount.Add(newAccount);
                await _userContext.SaveChangesAsync();

                var createdAccount = await _userContext.ExternalAccount.FindAsync(newAccount.accountId);

                return createdAccount;
            } 
            catch (IOException)
            {
                return null;
            }
        }

        public async Task<IEnumerable> GetAccounts(string userId)
        {
            return await _userContext.ExternalAccount.Where(x => x.userId == userId).ToListAsync();
        }
    }
}