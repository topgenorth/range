using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using NHibernate;


namespace Opgenorth.RangeLog.Core.Data.NHibernate
{
    public class Config
    {
        public static ISessionFactory CreateSessionFactory()
        {
            return Fluently.Configure()
                .Database(SQLiteConfiguration.Standard.UsingFile("RangeTrip.db3"))
                .Mappings(m =>
                          m.FluentMappings.AddFromAssemblyOf<RangeTripMap>())
                .BuildSessionFactory();
        }
    }
}