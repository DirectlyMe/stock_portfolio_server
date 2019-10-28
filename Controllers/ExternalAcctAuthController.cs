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
        public IActionResult LoginAccount(int id) // TODO: Add mfa code param, might need to turn this into a POST method
        {
            try
            {
                var userId = _userService.GetUserId(this.User);

                var userConnSpecs = _externalAcctService.Authorize(userId, id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}