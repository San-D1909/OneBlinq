using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class UserChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE `user` CHANGE COLUMN `ConfirmedEmail` `IsVerified` TINYINT(1) NOT NULL DEFAULT '0';");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"ALTER TABLE `user` CHANGE COLUMN `IsVerified` `ConfirmedEmail` TINYINT(1) NOT NULL DEFAULT '0';");
        }
    }
}
