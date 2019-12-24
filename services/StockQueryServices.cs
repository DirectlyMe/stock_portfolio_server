namespace stock_portfolio_server.services
{
    interface IStockQueryService
    {

    }
    public class StockQueryServices
    {
        private readonly string _baseUrl = "https://cloud.iexapis.com/stable/";
        public StockQueryServices() 
        {
            
        }
    }
}