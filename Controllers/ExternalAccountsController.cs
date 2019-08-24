using System.Threading.Tasks;
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
        private readonly UserService _userService;
        private readonly AccountService _accountService;

        public ExternalAccounts(UserDbContext userContext, UserService userService, AccountService accountService)
        {
            _userContext = userContext;
            _userService = userService;
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> GetExternalAccounts()
        {
            var accounts = await _accountService.GetAccounts(_userService.GetUserId(this.User));

            return Ok(accounts);
        }

        [HttpPost]
        public async Task<IActionResult> CreateExternalAccount(ExternalAccountView account)
        {
            if (ModelState.IsValid)
            {
                var newAccount = await _accountService.CreateAccount(
                    account.username, 
                    account.password, 
                    account.type, 
                    _userService.GetUserId(this.User)
                );

                if (newAccount == null)
                    return BadRequest(new { error = "account creation failed" });
                else
                    return Ok(new { success = "account created" });
            }
            else
            {
                return BadRequest(new { error = "data sent is not valid" });
            }
        }
    }
}