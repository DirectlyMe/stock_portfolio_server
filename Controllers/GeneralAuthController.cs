using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using stock_portfolio_server.Models;
using stock_portfolio_server.ViewModels;

namespace stock_portfolio_server.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class GeneralAuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public GeneralAuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost(Name = "register")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register([FromBody] RegisterModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user == null)
                {
                    user = new IdentityUser
                    {
                        Id = Guid.NewGuid().ToString(),
                        UserName = model.UserName,
                    };
                    var result = await _userManager.CreateAsync(user, model.Password);
                    
                    if (result.Errors != null)
                    {
                        var errorString = new StringBuilder();
                        foreach (var error in result.Errors) 
                        {
                            errorString.Append(error.Description);
                        }
                        return Content(errorString.ToString());
                    }
                }

                return StatusCode(200);
            }

            return StatusCode(304);
        }

        [HttpPost(Name = "login")]
        // [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login([FromBody] LoginModel model) 
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(model.UserName);
                var passValid = await _userManager.CheckPasswordAsync(user, model.Password);
                if (user != null && passValid)
                {
                    var identity = new ClaimsIdentity("cookies");
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.Id));
                    identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserName));

                    await HttpContext.SignInAsync("cookies", new ClaimsPrincipal(identity));

                    return StatusCode(200);
                }
                ModelState.AddModelError("", "Invalid UserName or Password");
            }
            return StatusCode(401);
        }
        
    }
}