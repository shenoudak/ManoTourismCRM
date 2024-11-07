using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManoTourism.Migrations
{
    public partial class addRoleIdField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "AspNetUsers",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5e479daa-e034-4990-acf0-c3245efc4584",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6a9e78b4-7b73-4cd4-ae99-d9097cbcc8ef", "AQAAAAEAACcQAAAAEH45EckqmanzQjiOYXjUSQw2Xgn9thJJb644Ub+HLdQm2l6RBUOtseTl+3fo41YBjA==", "706a8ced-e89e-47ba-8bd6-b0e4d85135d9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0325d4b-2a04-4d33-8e01-6f6f3afb3d8f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0e4edf23-5cb5-4d89-aa22-5829b13b6e83", "AQAAAAEAACcQAAAAEJXlkASywnvShi2U8h8QJSQONYfKzz/3cq/GhcDvUyhvoZ1tD+nkAT24RxP+gfo+Jg==", "acb88e5f-29e2-4d67-b84c-468d34032a39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "acd2498f-9519-400d-b43d-5a8ea7308cd5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "664c23b7-95d0-49d8-b93d-4069157dde18", "AQAAAAEAACcQAAAAENN/lIk0m1ZZwxkjApZykCwkAw5bi3KEgkr7bSQPpToA5BujkEBr+0NlrcsAaWOdrw==", "5b54703b-0646-43cf-b1ac-b00e82eba4d7" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "AspNetUsers");

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
    }
}
