using Microsoft.AspNetCore.Mvc;

namespace stock_portfolio_server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        public HomeController()
        {
            
        }

        [HttpGet]
        public string Get()
        {
            return "hello";
        }
    }
}