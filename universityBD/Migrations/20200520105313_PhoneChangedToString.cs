using Microsoft.EntityFrameworkCore.Migrations;

namespace universityBD.Migrations
{
    public partial class PhoneChangedToString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Students",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "Phone",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Students",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Phone",
                table: "Employees",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
