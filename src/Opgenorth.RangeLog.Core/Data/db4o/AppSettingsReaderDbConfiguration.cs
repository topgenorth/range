using System;
using System.Configuration;


namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public class AppSettingsReaderDbConfiguration : IDb4oConfiguration
    {
        public static readonly string ConnectionStringConfigKey = "connectionString";


        public string GetFilename()
        {
            return ReadSetting<string>(ConnectionStringConfigKey);
        }

        public string GetConnectionString()
        {
            return ReadSetting<string>(ConnectionStringConfigKey);
        }

        private static T ReadSetting<T>(string key)
        {
            var rdr = new AppSettingsReader();
            try
            {
                var value = (T) rdr.GetValue(key, typeof (T));
                return value;
            }
            catch (Exception ex)
            {
                var msg = ex.Message;
                throw new InvalidOperationException(msg, ex);
            }
        }
    }
}