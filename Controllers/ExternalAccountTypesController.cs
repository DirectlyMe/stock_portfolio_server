using System.Collections;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using stock_portfolio_server.services;
using stock_portfolio_server.ViewModels;

namespace stock_portfolio_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ExternalAccountTypesController : Controller
    {
        private readonly UserDbContext _userContext;

        public ExternalAccountTypesController(UserDbContext userDbContext)
        {
            _userContext = userDbContext;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok( new { accountTypes = await _userContext.AccountType.ToListAsync() });
        }
    }
}