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
    public class UsersController : Controller
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginModel userParam)
        {
            if (!ModelState.IsValid)
                return StatusCode(304);

            try
            {
                var user = await _userService.Authenticate(userParam.UserName, userParam.Password);
                return Ok(user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = "Username or password is incorrect", exception = ex });
            }
        }

        [AllowAnonymous]
        [HttpPost("register")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { error = "Model not valid" });

            try 
            {
                var user = await _userService.Register(model.UserName, model.Password);

                return Ok(new { message = "registration successful", user });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}