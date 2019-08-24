using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace stock_portfolio_server.Migrations
{
    public partial class ProductType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "type",
                table: "ExternalAccounts");

            migrationBuilder.AddColumn<int>(
                name: "typeId",
                table: "ExternalAccounts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AccountType",
                columns: table => new
                {
                    typeId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.typeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExternalAccounts_accountId",
                table: "ExternalAccounts",
                column: "accountId");

            migrationBuilder.CreateIndex(
                name: "IX_ExternalAccounts_typeId",
                table: "ExternalAccounts",
                column: "typeId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_name",
                table: "AccountType",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "IX_AccountType_typeId",
                table: "AccountType",
                column: "typeId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExternalAccounts_AccountType_typeId",
                table: "ExternalAccounts",
                column: "typeId",
                principalTable: "AccountType",
                principalColumn: "typeId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExternalAccounts_AccountType_typeId",
                table: "ExternalAccounts");

            migrationBuilder.DropTable(
                name: "AccountType");

            migrationBuilder.DropIndex(
                name: "IX_ExternalAccounts_accountId",
                table: "ExternalAccounts");

            migrationBuilder.DropIndex(
                name: "IX_ExternalAccounts_typeId",
                table: "ExternalAccounts");

            migrationBuilder.DropColumn(
                name: "typeId",
                table: "ExternalAccounts");

            migrationBuilder.AddColumn<string>(
                name: "type",
                table: "ExternalAccounts",
                type: "varchar(150)",
                nullable: true);
        }
    }
}
