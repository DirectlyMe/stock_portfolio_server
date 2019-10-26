using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using stock_portfolio_server.Models;
using stock_portfolio_server.ViewModels;

namespace stock_portfolio_server.services
{
    public interface IAccountService
    {
        Task<List<ExternalAccount>> GetAccounts(string userId);
        Task<List<ExternalAccountView>> GetAccountViews(string userId);
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
            var selectedType = await _userContext.AccountType.FirstAsync(e => e.name == type);

            if (selectedType == null)
                throw new Exception($"Account Type: '{type}' not found");

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

        public async Task<List<ExternalAccount>> GetAccounts(string userId)
        {
            return await _userContext.ExternalAccount.Where(x => x.userId == userId).ToListAsync();
        }

        public async Task<List<ExternalAccountView>> GetAccountViews(string userId)
        {
            return await _userContext.ExternalAccount.Where(x => x.userId == userId).Select(x => new ExternalAccountView{
                username = x.username,
                typeId = x.type.typeId,
                typeName = x.type.name
            }).ToListAsync();
        }
    }
}