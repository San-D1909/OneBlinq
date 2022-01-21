using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Backend.Migrations
{
    public partial class testing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PluginBundleImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PluginBundleId = table.Column<int>(type: "int", nullable: true),
                    imagedata = table.Column<string>(type: "LONGTEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginBundleImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PluginBundleImage_PluginBundle_PluginBundleId",
                        column: x => x.PluginBundleId,
                        principalTable: "PluginBundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PluginBundleImage_PluginBundleId",
                table: "PluginBundleImage",
                column: "PluginBundleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PluginBundleImage");
        }
    }
}
