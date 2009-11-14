using Opgenorth.RangeLog.Core.Data.db4o;

using StructureMap.Configuration.DSL;


namespace Opgenorth.RangeLog.Core.Container.StructureMap
{
    public class DefaultStructureMapRegistry : Registry
    {
        public DefaultStructureMapRegistry()
        {
            ForRequestedType<IDb4oConfiguration>()
                .TheDefaultIsConcreteType<SimpleDb4oConfiguration>();
            ForRequestedType<IDb4oConnectionFactory>()
                .TheDefaultIsConcreteType<Db4oConnectionFactory>();
            ForRequestedType<ICreateDb4oDatabase>()
                .TheDefaultIsConcreteType<CreateDb4oDatabaseCommand>();
            ForRequestedType<IDb4oTripRepository>()
                .TheDefaultIsConcreteType<Db4oRepository>();
        }
    }
}