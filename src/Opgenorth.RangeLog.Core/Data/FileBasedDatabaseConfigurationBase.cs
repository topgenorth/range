using System;
using System.IO;


namespace Opgenorth.RangeLog.Core.Data
{
    public abstract class FileBasedDatabaseConfigurationBase : IFilebaseDatabaseConfiguration
    {
        protected FileBasedDatabaseConfigurationBase(string fileName)
        {
            FileName = fileName;
        }

        protected string FileName { get; private set; }

        public virtual string GetFilename()
        {
            return Path.Combine(GetDataDirectory(), FileName);
        }

        public abstract string GetDataDirectory();
    }

    public abstract class FileBasedDatabaseStoredInLocalApplicationDataFolderBase : FileBasedDatabaseConfigurationBase
    {
        protected FileBasedDatabaseStoredInLocalApplicationDataFolderBase(string fileName) : base(fileName)
        {
        }

        public override string GetDataDirectory()
        {
            var appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appDataDir, "RangeLog");
            return dir;
        }
    }
}