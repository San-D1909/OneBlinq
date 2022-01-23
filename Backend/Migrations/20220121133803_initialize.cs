using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Backend.Migrations
{
    public partial class initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Company",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CompanyName = table.Column<string>(type: "text", nullable: true),
                    ZipCode = table.Column<string>(type: "text", nullable: true),
                    Street = table.Column<string>(type: "text", nullable: true),
                    HouseNumber = table.Column<string>(type: "text", nullable: true),
                    Country = table.Column<string>(type: "text", nullable: true),
                    BTWNumber = table.Column<string>(type: "text", nullable: true),
                    KVKNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Company", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Device",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    DeviceToken = table.Column<string>(type: "varchar(767)", nullable: false),
                    DeviceName = table.Column<string>(type: "text", nullable: false),
                    LicenseId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Device", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Plugin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PluginName = table.Column<string>(type: "text", nullable: false),
                    PluginDescription = table.Column<string>(type: "text", nullable: false),
                    StripeProductId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plugin", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PluginBundle",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    BundleName = table.Column<string>(type: "text", nullable: false),
                    BundleDescription = table.Column<string>(type: "text", nullable: false),
                    StripeProductId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginBundle", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResetToken",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResetToken", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    IsVerified = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<byte[]>(type: "varbinary(4000)", nullable: false),
                    IsAdmin = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CompanyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Company_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Company",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PluginImage",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PluginId = table.Column<int>(type: "int", nullable: true),
                    imagedata = table.Column<string>(type: "LONGTEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PluginImage_Plugin_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PluginVariant",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PluginId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    StripePriceId = table.Column<string>(type: "text", nullable: true),
                    MaxActivations = table.Column<int>(type: "int", nullable: false),
                    IsSubscription = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginVariant", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PluginVariant_Plugin_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateTable(
                name: "PluginBundles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PluginBundleId = table.Column<int>(type: "int", nullable: false),
                    PluginId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginBundles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PluginBundles_Plugin_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PluginBundles_PluginBundle_PluginBundleId",
                        column: x => x.PluginBundleId,
                        principalTable: "PluginBundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "License",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LicenseKey = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    VariantId = table.Column<int>(type: "int", nullable: true),
                    IsActive = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CreationTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    TimesActivated = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_License", x => x.Id);
                    table.ForeignKey(
                        name: "FK_License_PluginVariant_VariantId",
                        column: x => x.VariantId,
                        principalTable: "PluginVariant",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_License_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PluginLicense",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LicenseId = table.Column<int>(type: "int", nullable: false),
                    PluginId = table.Column<int>(type: "int", nullable: true),
                    PluginBundleId = table.Column<int>(type: "int", nullable: true),
                    TimesActivated = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PluginLicense", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PluginLicense_License_LicenseId",
                        column: x => x.LicenseId,
                        principalTable: "License",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PluginLicense_Plugin_PluginId",
                        column: x => x.PluginId,
                        principalTable: "Plugin",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PluginLicense_PluginBundle_PluginBundleId",
                        column: x => x.PluginBundleId,
                        principalTable: "PluginBundle",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UNIQUE_DEVICE",
                table: "Device",
                columns: new[] { "LicenseId", "DeviceToken" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_License_UserId",
                table: "License",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_License_VariantId",
                table: "License",
                column: "VariantId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginBundleImage_PluginBundleId",
                table: "PluginBundleImage",
                column: "PluginBundleId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginBundles_PluginId",
                table: "PluginBundles",
                column: "PluginId");

            migrationBuilder.CreateIndex(
                name: "IX_UNIQUE_PLUGINBUNDLE",
                table: "PluginBundles",
                columns: new[] { "PluginBundleId", "PluginId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PluginImage_PluginId",
                table: "PluginImage",
                column: "PluginId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginLicense_PluginBundleId",
                table: "PluginLicense",
                column: "PluginBundleId");

            migrationBuilder.CreateIndex(
                name: "IX_PluginLicense_PluginId",
                table: "PluginLicense",
                column: "PluginId");

            migrationBuilder.CreateIndex(
                name: "IX_UNIQUE_PLUGINLICENSE",
                table: "PluginLicense",
                columns: new[] { "LicenseId", "PluginId", "PluginBundleId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PluginVariant_PluginId",
                table: "PluginVariant",
                column: "PluginId");

            migrationBuilder.CreateIndex(
                name: "IX_User_CompanyId",
                table: "User",
                column: "CompanyId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Device");

            migrationBuilder.DropTable(
                name: "PluginBundleImage");

            migrationBuilder.DropTable(
                name: "PluginBundles");

            migrationBuilder.DropTable(
                name: "PluginImage");

            migrationBuilder.DropTable(
                name: "PluginLicense");

            migrationBuilder.DropTable(
                name: "ResetToken");

            migrationBuilder.DropTable(
                name: "License");

            migrationBuilder.DropTable(
                name: "PluginBundle");

            migrationBuilder.DropTable(
                name: "PluginVariant");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Plugin");

            migrationBuilder.DropTable(
                name: "Company");
        }
    }
}
