namespace Datam.Core.MySql
{
    public static class EmbeddedAppScripts
    {
        public static string CREATE_TABLE_SCRIPT_HISTORY =
@"CREATE TABLE scripts_history 
(
    filename VARCHAR(255) NOT NULL PRIMARY KEY,
    date_time_applied DATETIME NOT NULL
)";

        public static string CREATE_GET_MIGRATION_INFO =
@"CREATE PROCEDURE get_migration_info
()
BEGIN
SELECT filename, date_time_applied
FROM scripts_history
ORDER BY date_time_applied;
END";

        public static string CREATE_UPGRADE_VERSION =
@"CREATE PROCEDURE upgrade_version
(
	IN fname VARCHAR(255)
)
BEGIN
	INSERT INTO scripts_history (filename, date_time_applied) 
	VALUES(fname, NOW());
END";

        public static string CREATE_HAS_SCRIPT_EXECUTED =
@"CREATE PROCEDURE has_script_executed
(
    IN fname VARCHAR(255)
)
BEGIN
    IF EXISTS(SELECT * FROM scripts_history WHERE filename = fname) THEN
        SELECT 1;
    ELSE
        SELECT 0;
	END IF;
END";
    }
}
