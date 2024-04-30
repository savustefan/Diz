using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LucrareDisertatie.Migrations.LoginDb
{
    /// <inheritdoc />
    public partial class authentificationdbcontextmigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "156239b5-c8a8-4917-bb01-4c9aac58df7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "28717b1e-d7c3-4303-916a-055b4aeb9639", "AQAAAAIAAYagAAAAEMMK331sMuCntyDwKf7RfWDjcgYGOcM0FNqopa+Eh8KpSXCRsK5+3YmAwZ1goASi4w==", "443eb47c-e579-43f7-ab61-7ba96dbc5187" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "156239b5-c8a8-4917-bb01-4c9aac58df7e",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a8032fc4-3c8a-4302-ae34-f2289508dac5", "AQAAAAIAAYagAAAAEHIkbvWlWTWbt4ymaiElngc9Bhg36iJBo86lYrd+TaiyFiGkbe6M9SEm037wRq8t0A==", "1aa2c1ec-7b31-44f5-aaac-415ef5f2ca31" });
        }
    }
}
