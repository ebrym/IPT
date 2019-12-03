using Microsoft.EntityFrameworkCore.Migrations;

namespace IPT.Data.Migrations
{
    public partial class AmountDeducted : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "TotalAmmountDeducted",
                table: "TaxPayers",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TotalAmmountDeducted",
                table: "TaxPayers");
        }
    }
}
