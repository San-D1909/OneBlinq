using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class DecreaseCountAfterDeleteTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS `DecreaseCountAfterDeviceDeleteTrigger`;");
            migrationBuilder.Sql(@"CREATE TRIGGER `DecreaseCountAfterDelete` AFTER DELETE ON `device` FOR EACH ROW BEGIN
DECLARE timesActivated, licenseId, maxCount, pluginId INT;

SET @licenseId = OLD.LicenseId;
SET @pluginId = OLD.PluginId;
    
SELECT pl.TimesActivated
INTO @timesActivated
FROM pluginlicenses AS pl
WHERE pl.PluginId = @pluginId && pl.LicenseId = @licenseId;

SET @timesActivated = @timesActivated - 1;

SELECT lt.MaxAmount
INTO @maxCount
FROM license AS l
INNER JOIN licensetype AS lt
ON lt.Id = l.LicenseTypeId
WHERE l.Id = @licenseId;

UPDATE pluginlicenses as pl
SET pl.TimesActivated= @timesactivated
WHERE pl.PluginId = @pluginId && pl.LicenseId = @licenseId;

END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS `DecreaseCountAfterDeviceDeleteTrigger`;");
        }
    }
}
