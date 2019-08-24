using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace stock_portfolio_server.Models
{
    public class ExternalAccount
    {
        public int accountId { get; set; }
        public string userId { get; set; }
        public string username { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        public User user { get; set; }
        public virtual AccountType type { get; set; }
    }
}