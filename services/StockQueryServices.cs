using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using stock_portfolio_server.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using stock_portfolio_server.services;
using stock_portfolio_server.ViewModels;
using System.Security.Claims;

namespace stock_portfolio_server.services
{
    public interface IStockQueryService
    {
        Task<List<Stock>> GetUserStocks(string userId);
    }

    public class StockQueryServices : IStockQueryService
    {
        private readonly UserDbContext _userContext;
        private readonly string _baseUrl = "https://cloud.iexapis.com/stable/";
        public StockQueryServices(UserDbContext userContext) 
        {
            _userContext = userContext;
        }

        public async Task<List<Stock>> GetUserStocks(string userId) 
        {
            return await _userContext.Stocks.Where(stock => stock.userId == userId).ToListAsync();
        }
    }
}