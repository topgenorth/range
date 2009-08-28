using System;
using System.IO;

using Opgenorth.RangeLog.Core.Data.db4o;


namespace Opgenorth.RangeLog.Tests.Data.db4o
{
    public class UnitTestingDb4oConfiguration : IDb4oConfiguration
    {
        public static readonly string DatabaseName = "UnitTestingRangeLog.db";


        private static string GetDataDirectory()
        {
            var appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appDataDir, "RangeLog");
            return dir;
        }

        public string GetFilename()
        {
            return Path.Combine(GetDataDirectory(), DatabaseName);
        }
    }
}