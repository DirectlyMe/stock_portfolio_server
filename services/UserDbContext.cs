using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using stock_portfolio_server.Models;

namespace stock_portfolio_server.services
{
    public class UserDbContext : IdentityDbContext<User>
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(user => user.HasIndex(x => x.Token).IsUnique(false));
            builder.Entity<RobinhoodUser>(user => {
                user.ToTable("RobinhoodUsers");
                user.HasKey(x => x.userId);

                user.HasOne<User>().WithOne().HasForeignKey<RobinhoodUser>(x => x.userId).IsRequired(true);
            });
        }
    }
}