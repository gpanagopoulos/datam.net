namespace Datam.Core.SqlServer
{
    public static class EmbeddedAppScripts
    {
        public static string CREATE_SCHEMA_MIGRATIONS =
            @"CREATE SCHEMA [migrations] 
              GO";

        public static string CREATE_TABLE_SCRIPT_HISTORY = 
            @"CREATE TABLE [migrations].[scripts_history] 
              (
                [Filename] VARCHAR(255) NOT NULL PRIMARY KEY,
                [DateTimeApplied] DATETIME NOT NULL
              )
              GO";

        public static string CREATE_GET_MIGRATION_INFO =
            @"CREATE PROCEDURE [migrations].[GetMigrationInfo]
              AS
              BEGIN
                SELECT [Filename], [DateTimeApplied]
                FROM [migrations].[scripts_history]
                ORDER BY [DateTimeApplied]
              END
              GO";

        public static string CREATE_UPGRADE_VERSION =
            @"CREATE PROCEDURE[migrations].[UpgradeVersion]
            (
                @filename VARCHAR(255)
            )
            AS
            BEGIN
                INSERT INTO[migrations].[scripts_history] ([Filename], [DateTimeApplied]) 
                VALUES(@filename, GETDATE())
            END
            GO";

        public static string CREATE_HAS_SCRIPT_EXECUTED = 
            @"CREATE PROCEDURE [migrations].[HasScriptExecuted]
            (
                @filename VARCHAR(255)
            )
            AS
            BEGIN
                IF EXISTS(SELECT* FROM [migrations].[scripts_history] WHERE [Filename]= @filename)
                    SELECT 1 
                ELSE
                    SELECT 0
            END
            GO";
    }
}
