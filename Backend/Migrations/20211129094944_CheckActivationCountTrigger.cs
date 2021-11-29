using Microsoft.EntityFrameworkCore.Migrations;

namespace Backend.Migrations
{
    public partial class CheckActivationCountTrigger : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS `CheckActivationCount`;");
            migrationBuilder.Sql(@"
CREATE DEFINER=`root`@`localhost` TRIGGER `CheckActivationCount` BEFORE UPDATE ON `pluginlicense` FOR EACH ROW BEGIN
    DECLARE
        maxCount,
        PluginId INT ; DECLARE LicenseId VARCHAR(18) ;
    SELECT
        lt.MaxAmount
    INTO @maxCount
FROM
    license AS l
INNER JOIN licensetype AS lt
ON
    lt.Id = l.LicenseTypeId ; IF NEW.timesActivated > @maxCount THEN SIGNAL SQLSTATE '78282'
SET MESSAGE_TEXT
    = 'Activation linmit reached!' ;
END IF ;
            END;");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP TRIGGER IF EXISTS `CheckActivationCount`;");
        }
    }
}
