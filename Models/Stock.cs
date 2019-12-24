namespace stock_portfolio_server.Models
{
    public class Stock
    {
        public string Symbol { get; set; }
        public int StockId { get; set; }
        public string userId { get; set; }
        public User user { get; set; }
    }
}