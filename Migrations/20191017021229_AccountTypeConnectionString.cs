using Microsoft.EntityFrameworkCore.Migrations;

namespace stock_portfolio_server.Migrations
{
    public partial class AccountTypeConnectionString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "connectionUrl",
                table: "AccountType",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_connectionUrl",
                table: "AccountType",
                column: "connectionUrl");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AccountType_connectionUrl",
                table: "AccountType");

            migrationBuilder.DropColumn(
                name: "connectionUrl",
                table: "AccountType");
        }
    }
}
