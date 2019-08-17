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
            if (ModelState.IsValid){
                var user = await _userService.Authenticate(userParam.UserName, userParam.Password);

                if (user == null)
                    return BadRequest(new { error = "Username or password is incorrect" });

                return Ok(user);
            }

            return StatusCode(304);
        }
        
        [AllowAnonymous]
        [HttpPost("register")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                var errors = await _userService.Register(model.UserName, model.Password);

                if (errors == null) {
                    return Ok(new { message = "registration successful" });
                }

                return BadRequest(new { error = errors });
            }

            return StatusCode(304);
        }
    }
}