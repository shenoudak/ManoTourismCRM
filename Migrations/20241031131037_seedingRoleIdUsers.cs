using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManoTourism.Migrations
{
    public partial class seedingRoleIdUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5e479daa-e034-4990-acf0-c3245efc4584",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId", "SecurityStamp" },
                values: new object[] { "073a213d-5737-48d4-ab30-7eb807aa30e9", "AQAAAAEAACcQAAAAEHH5iUnAbG68nQh2bqBNSR+IE5Kx/jc8m5+05T/f4U+LNPF8PLk7wZ+BzzC/rZAaDg==", 3, "09d130d9-3832-4d9c-9c53-5b9036a14f6b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0325d4b-2a04-4d33-8e01-6f6f3afb3d8f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId", "SecurityStamp" },
                values: new object[] { "1facd508-e79c-4ed2-b89c-573f628d9fe2", "AQAAAAEAACcQAAAAEEbK15qDo19NZ0LrI7403qg8Zddo+KbFBKd6Ham4BPGY3TV9eWxudmjxKLsgA15k2g==", 6, "5b66c382-9cb8-4f4d-9563-5c10d6bd26d6" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "acd2498f-9519-400d-b43d-5a8ea7308cd5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId", "SecurityStamp" },
                values: new object[] { "f3003a9e-866f-4a9c-942b-d9741831604d", "AQAAAAEAACcQAAAAEJta+Qg/r16lfHQmRmoRJfOhWc6cnh3mCw6C3qKmpRvQaMu2XcdC8hGST5fgwrSomg==", 1, "c0c7fee9-309a-4737-9bae-664468165f38" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5e479daa-e034-4990-acf0-c3245efc4584",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId", "SecurityStamp" },
                values: new object[] { "6a9e78b4-7b73-4cd4-ae99-d9097cbcc8ef", "AQAAAAEAACcQAAAAEH45EckqmanzQjiOYXjUSQw2Xgn9thJJb644Ub+HLdQm2l6RBUOtseTl+3fo41YBjA==", 0, "706a8ced-e89e-47ba-8bd6-b0e4d85135d9" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a0325d4b-2a04-4d33-8e01-6f6f3afb3d8f",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId", "SecurityStamp" },
                values: new object[] { "0e4edf23-5cb5-4d89-aa22-5829b13b6e83", "AQAAAAEAACcQAAAAEJXlkASywnvShi2U8h8QJSQONYfKzz/3cq/GhcDvUyhvoZ1tD+nkAT24RxP+gfo+Jg==", 0, "acb88e5f-29e2-4d67-b84c-468d34032a39" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "acd2498f-9519-400d-b43d-5a8ea7308cd5",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RoleId", "SecurityStamp" },
                values: new object[] { "664c23b7-95d0-49d8-b93d-4069157dde18", "AQAAAAEAACcQAAAAENN/lIk0m1ZZwxkjApZykCwkAw5bi3KEgkr7bSQPpToA5BujkEBr+0NlrcsAaWOdrw==", 0, "5b54703b-0646-43cf-b1ac-b00e82eba4d7" });
        }
    }
}
