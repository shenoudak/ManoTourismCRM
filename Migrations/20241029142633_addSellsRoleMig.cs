using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManoTourism.Migrations
{
    public partial class addSellsRoleMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7031feee-d1ce-4941-8cd3-61f4c30ca256", "7031feee-d1ce-4941-8cd3-61f4c30ca256", "selles", "selles" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5e479daa-e034-4990-acf0-c3245efc4584",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1d9a14d5-7174-4733-88d3-4502228a5085", "AQAAAAEAACcQAAAAEHXKkEEEg258xi/2/nyDcKSc7PCQn/EsJ3zBMfOsCTbhKTrZ4Bvm0LrMNRno1eU93A==", "eccb6a25-6440-4111-8b1d-d2211a226145" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0325d4b-2a04-4d33-8e01-6f6f3afb3d8f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c49dd7ac-021a-44e3-8db2-c25cace46657", "AQAAAAEAACcQAAAAECpPoVTCz17PbdVr9ND0bW2i+hAzAhcxOgnHa8pYiNbiHfHCKUrEnGGt0Bct9afKqw==", "52e07f7b-3920-480c-8871-2f8b2a6218e7" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "acd2498f-9519-400d-b43d-5a8ea7308cd5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1119ecfa-fdf6-4eae-9e9a-44c1afa35dfe", "AQAAAAEAACcQAAAAEJxpuMrPAWQrW+rkgdKXY/KXKhbeRCWTO0xJq6lI5n1wpOirZkIq7TiNyri3I651sw==", "b96d213b-983d-46e3-ac25-08c05400e033" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7031feee-d1ce-4941-8cd3-61f4c30ca256");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5e479daa-e034-4990-acf0-c3245efc4584",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aadbe1ce-e1cd-4c3b-9cf0-b0ceb268ccf6", "AQAAAAEAACcQAAAAEPyVEbwkGiWuwk6tOMUNKS1fuX0ycASLNvDlINHG2S4+bRwv+OusJu31asbHDHL5qA==", "83dbe2bf-5b8a-44fb-a3db-8d4a26d0152c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0325d4b-2a04-4d33-8e01-6f6f3afb3d8f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d8b2860d-e6cd-4f1e-aa34-210695509530", "AQAAAAEAACcQAAAAEHDQ/dA9O5pnmB0hWdgYVh5Wb2aff3nn6oFyoL/nHXb4huEge7FJf0wmQmDD2aMEkQ==", "9ed22287-6347-47ae-b226-e08702f1d8dc" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "acd2498f-9519-400d-b43d-5a8ea7308cd5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2c3ac319-076b-4ea3-8bc4-034906a164c4", "AQAAAAEAACcQAAAAEF+tZ705pCOpzmCmDGX41JjhJRnmCtXJ//Yz8qZFwmgStLIr6jHA4qBcM67JTLv2yQ==", "ac287ef7-b80c-4905-8424-9a29769e3f81" });
        }
    }
}
