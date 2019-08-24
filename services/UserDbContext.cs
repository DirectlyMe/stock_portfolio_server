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

        public virtual DbSet<ExternalAccount> ExternalAccount { get; set; }
        public virtual DbSet<AccountType> AccountType { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>(user => user.HasIndex(x => x.Token).IsUnique(false));
            builder.Entity<ExternalAccount>(externalAccount =>
            {
                externalAccount.ToTable("ExternalAccounts");

                externalAccount.HasIndex(x => x.accountId);
                externalAccount.HasIndex(x => x.userId);

                externalAccount.HasKey(x => x.accountId)
                            .HasName("PRIMARY");

                externalAccount.Property(e => e.accountId)
                            .HasColumnName("accountId")
                            .HasColumnType("int(11)");

                externalAccount.Property(e => e.userId)
                            .HasColumnName("userId")
                            .HasColumnType("varchar(150)");

                externalAccount.Property(x => x.username)
                            .HasColumnName("username")
                            .HasColumnType("varchar(150)");

                externalAccount.Property(e => e.password)
                            .HasColumnName("password")
                            .HasColumnType("varchar(150)");

                externalAccount.HasOne(e => e.user)
                            .WithMany(b => b.externalAccounts)
                            .HasForeignKey(p => p.userId);

                externalAccount.HasOne(e => e.type);
            });

            builder.Entity<AccountType>(accountType => {
                accountType.HasIndex(x => x.name);
                accountType.HasIndex(x => x.typeId);

                accountType.ToTable("AccountType");

                accountType.HasKey(x => x.typeId)
                           .HasName("PRIMARY");

                accountType.Property(e => e.name)
                           .HasColumnName("name");
                
                accountType.Property(e => e.typeId)
                           .HasColumnName("typeId");
            });
        }
    }
}