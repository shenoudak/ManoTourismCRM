using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManoTourism.Migrations.Mano
{
    public partial class addIsPaidFieldToRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPaid",
                table: "Requests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPaid",
                table: "Requests");
        }
    }
}
