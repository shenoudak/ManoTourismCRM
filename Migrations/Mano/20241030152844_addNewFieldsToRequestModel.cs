using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManoTourism.Migrations.Mano
{
    public partial class addNewFieldsToRequestModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SellesId",
                table: "Requests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SharedEmployeeeId",
                table: "Requests",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SellesId",
                table: "Requests");

            migrationBuilder.DropColumn(
                name: "SharedEmployeeeId",
                table: "Requests");
        }
    }
}
