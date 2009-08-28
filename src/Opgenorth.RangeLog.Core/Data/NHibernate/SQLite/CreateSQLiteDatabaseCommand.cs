using System.IO;

using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;


namespace Opgenorth.RangeLog.Core.Data.NHibernate.SQLite
{
    public class CreateSQLiteDatabaseCommand : ICommand
    {
        private readonly IFilebaseDatabaseConfiguration _dbConfig;
        private readonly Configuration _nhibernateConfig;

        public CreateSQLiteDatabaseCommand(IFilebaseDatabaseConfiguration dbConfig, Configuration nhibernateConfig)
        {
            _dbConfig = dbConfig;
            _nhibernateConfig = nhibernateConfig;
        }

        public void Execute()
        {
            BuildSchema(_nhibernateConfig);
        }

        public void BuildSchema(Configuration config)
        {
            if (File.Exists(_dbConfig.GetFilename()))
            {
                File.Delete(_dbConfig.GetFilename());
            }

            new SchemaExport(config).Create(false, true);
        }
    }
}