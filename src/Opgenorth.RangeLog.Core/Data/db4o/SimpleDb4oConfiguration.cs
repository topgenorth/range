using System;
using System.IO;


namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public class SimpleDb4oConfiguration : FileBasedDatabaseStoredInLocalApplicationDataFolderBase, IDb4oConfiguration
    {
//        public static readonly string DatabaseName = "RangeLog.db";


        public SimpleDb4oConfiguration() : base("RangeLog.db")
        {
        }
    }
}