using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stock_portfolio_server.services;
using stock_portfolio_server.ViewModels;
using System.Security.Claims;

namespace stock_portfolio_server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserStocksController : Controller
    {
        private readonly UserDbContext _userContext;
        private readonly IUserService _userService;
        private readonly IStockQueryService _stockService;

        public UserStocksController(UserDbContext userContext, IUserService userService, IStockQueryService stockService)
        {
            _userContext = userContext;
            _userService = userService;
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUserStocks() 
        {
            var stocks = await _stockService.GetUserStocks(_userService.GetUserId(this.User));

            return Ok(new { stocks = stocks });
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateUserStock([FromBody] StockSubmitView stock)
        {
            try 
            {
                if (!ModelState.IsValid)
                    throw new Exception("Data sent is not valid");

                var existingStocks = await _stockService.GetUserStocks(_userService.GetUserId(this.User));

                return Ok(new { success = "stock added" });
            } 
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }

        }
    }
}