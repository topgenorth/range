using System;
using System.IO;
using Db4objects.Db4o;

namespace Opgenorth.RangeLog.Core.Data.db4o
{
    [Obsolete]
    public class ContainerTools : IContainerTools
    {
        public string ContainerName
        {
            get { return Path.Combine(GetDefaultDataDirectory(), "RangeLog.db"); }
        }

        private static string GetDefaultDataDirectory()
        {
            var appDataDir = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dir = Path.Combine(appDataDir, "RangeLog");
            return dir;
        }

        public void CreateDataDirectory()
        {
            if (!Directory.Exists(GetDefaultDataDirectory()))
            {
                Directory.CreateDirectory(GetDefaultDataDirectory());
            }
        }

        public IObjectContainer GetDb4oContainer()
        {
            return Db4oFactory.OpenFile(ContainerName);
        }
    }
}