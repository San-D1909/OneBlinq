using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class userSaltChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Salt",
                table: "User",
                type: "varbinary(4000)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Salt",
                table: "User",
                type: "text",
                nullable: false,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(4000)");
        }
    }
}
