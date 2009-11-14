namespace Opgenorth.RangeLog.Core.Data.NHibernate.SQLite
{
    public class SQLiteDatabaseConfiguration: FileBasedDatabaseStoredInLocalApplicationDataFolderBase
    {
        public SQLiteDatabaseConfiguration() : base("RangeLog.sqlite3")
        {
        }
    }
}