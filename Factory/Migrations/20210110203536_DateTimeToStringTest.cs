using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Factory.Migrations
{
    public partial class DateTimeToStringTest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "InstallDate",
                table: "Machines",
                nullable: true,
                oldClrType: typeof(DateTime));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "InstallDate",
                table: "Machines",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
