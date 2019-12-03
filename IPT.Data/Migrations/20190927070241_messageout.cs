using Microsoft.EntityFrameworkCore.Migrations;

namespace IPT.Data.Migrations
{
    public partial class messageout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Operator",
                table: "MessageOuts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Receiver",
                table: "MessageOuts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operator",
                table: "MessageOuts");

            migrationBuilder.DropColumn(
                name: "Receiver",
                table: "MessageOuts");
        }
    }
}
