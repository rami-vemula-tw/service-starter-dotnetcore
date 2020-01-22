using Microsoft.EntityFrameworkCore.Migrations;

namespace PaymentApi.Migrations
{
    public partial class paymentv2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "_status",
                table: "Payments",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "_status",
                table: "Payments");
        }
    }
}
