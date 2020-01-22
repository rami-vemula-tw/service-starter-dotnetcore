using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace PaymentApi.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Payments",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    _beneficiaryName = table.Column<string>(nullable: true),
                    _beneficiaryAccountNumber = table.Column<string>(nullable: true),
                    _beneficiaryIfscCode = table.Column<string>(nullable: true),
                    _payeeName = table.Column<string>(nullable: true),
                    _payeeAccountNumber = table.Column<string>(nullable: true),
                    _payeeIfscCode = table.Column<string>(nullable: true),
                    _amount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payments", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Payments");
        }
    }
}
