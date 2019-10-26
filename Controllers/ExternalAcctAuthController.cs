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

        public ExternalAcctAuthController(UserDbContext userContext, IUserService userService, IAccountService accountService)
        {
            _userContext = userContext;
            _userService = userService;
            _accountService = accountService;
        }

        public async Task<IActionResult> AccountLogin([FromBody] ExternalAccountLogin loginModel)
        {
            try
            {
                if (!ModelState.IsValid)
                    throw new Exception("Model not valid");
            }
            catch (Exception ex) 
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}