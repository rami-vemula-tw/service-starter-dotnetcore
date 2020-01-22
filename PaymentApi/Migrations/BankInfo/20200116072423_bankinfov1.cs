using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentApi.Migrations.BankInfo
{
    public partial class bankinfov1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BankInfo",
                columns: table => new
                {
                    BankCode = table.Column<string>(nullable: false),
                    AccountAPIUrl = table.Column<string>(nullable: true),
                    PaymentAPIURL = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BankInfo", x => x.BankCode);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BankInfo");
        }
    }
}
