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
    public class ExternalAccountTypesController
    {
        
    }
}