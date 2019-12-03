using Microsoft.EntityFrameworkCore.Migrations;

namespace IPT.Data.Migrations
{
    public partial class messgeIn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Message",
                table: "MessageOuts",
                newName: "msg");

            migrationBuilder.RenameColumn(
                name: "SMSText",
                table: "MessageIns",
                newName: "sender");

            migrationBuilder.AddColumn<string>(
                name: "Operator",
                table: "MessageIns",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "msg",
                table: "MessageIns",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "msgtype",
                table: "MessageIns",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "receiver",
                table: "MessageIns",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Operator",
                table: "MessageIns");

            migrationBuilder.DropColumn(
                name: "msg",
                table: "MessageIns");

            migrationBuilder.DropColumn(
                name: "msgtype",
                table: "MessageIns");

            migrationBuilder.DropColumn(
                name: "receiver",
                table: "MessageIns");

            migrationBuilder.RenameColumn(
                name: "msg",
                table: "MessageOuts",
                newName: "Message");

            migrationBuilder.RenameColumn(
                name: "sender",
                table: "MessageIns",
                newName: "SMSText");
        }
    }
}
