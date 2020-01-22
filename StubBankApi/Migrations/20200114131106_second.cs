using Microsoft.EntityFrameworkCore.Migrations;

namespace StubBankApi.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems");

            migrationBuilder.RenameTable(
                name: "TodoItems",
                newName: "Accounts");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts",
                column: "AccountNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Accounts",
                table: "Accounts");

            migrationBuilder.RenameTable(
                name: "Accounts",
                newName: "TodoItems");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TodoItems",
                table: "TodoItems",
                column: "AccountNumber");
        }
    }
}
