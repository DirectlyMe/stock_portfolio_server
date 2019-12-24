using System;
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
    public class ExternalAcctAuthController : Controller
    {
        private readonly UserDbContext _userContext;
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;
        private readonly IExternalAccountService _externalAcctService;

        public ExternalAcctAuthController(UserDbContext userContext, IUserService userService, IAccountService accountService, IExternalAccountService externalAcctService)
        {
            _userContext = userContext;
            _userService = userService;
            _accountService = accountService;
            _externalAcctService = externalAcctService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> LoginAccount(int id)
        {
            try
            {
                var userId = _userService.GetUserId(this.User);

                var userConnSpecs = await _externalAcctService.Authorize(userId, id);

                return Ok(userConnSpecs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpGet("{id}/{mfa}")]
        public async Task<IActionResult> LoginAccountMfa(int id, int mfa) 
        {
            try
            {
                var userId = _userService.GetUserId(this.User);

                var userConnSpecs = await _externalAcctService.Authorize(userId, id, mfa);

                return Ok(userConnSpecs);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}