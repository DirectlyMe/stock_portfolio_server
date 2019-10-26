using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stock_portfolio_server.services;
using stock_portfolio_server.ViewModels;

namespace stock_portfolio_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalAccounts : Controller
    {
        private readonly UserDbContext _userContext;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public ExternalAccounts(UserDbContext userContext, IUserService userService, IAccountService accountService)
        {
            _userContext = userContext;
            _userService = userService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExternalAccounts()
        {
            var accounts = await _accountService.GetAccounts(_userService.GetUserId(this.User));

            return Ok(new { accounts = accounts });
        }

        [HttpPost]
        public async Task<IActionResult> CreateExternalAccount([FromBody] ExternalAccountSubmittionView account)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("data sent is not valid");

                var existingAccounts = await _accountService.GetAccounts(_userService.GetUserId(this.User));

                if(existingAccounts.Exists(acct => acct.type.name == account.type))
                    throw new Exception($"Account already exists for this {this.User}");

                var newAccount = await _accountService.CreateAccount(
                    account.username,
                    account.password,
                    account.type,
                    _userService.GetUserId(this.User)
                );
                

                return Ok(new { success = "account created" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
