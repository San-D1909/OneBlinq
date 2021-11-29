using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class IncreaseCountAfterDeviceInsertTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS `IncreaseCountAfterInsert`;");
            migrationBuilder.Sql(@"CREATE TRIGGER `IncreaseCountAfterInsert` AFTER INSERT ON `device` FOR EACH ROW BEGIN
DECLARE timesActivated, licenseId, maxCount, pluginId INT;

SET @licenseId = NEW.LicenseId;
SET @pluginId = NEW.PluginId;
    
SELECT pl.TimesActivated
INTO @timesActivated
FROM pluginlicenses AS pl
WHERE pl.PluginId = @pluginId && pl.LicenseId = @licenseId;

SET @timesActivated = @timesActivated + 1;

SELECT lt.MaxAmount
INTO @maxCount
FROM license AS l
INNER JOIN licensetype AS lt
ON lt.Id = l.LicenseTypeId
WHERE l.Id = @licenseId;

UPDATE pluginlicenses as pl
SET pl.TimesActivated= @timesactivated
WHERE pl.PluginId = @pluginId && pl.LicenseId = @licenseId;

END; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS `IncreaseCountAfterInsert`;");
        }
    }
}
