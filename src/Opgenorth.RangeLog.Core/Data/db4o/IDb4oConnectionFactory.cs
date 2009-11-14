using Db4objects.Db4o;


namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public interface IDb4oConnectionFactory
    {
        IDb4oConfiguration Configuration { get; }


        IObjectContainer CreateObjectContainer();
        IDb4oObjectFinder CreateFinder();
        IDb4oObjectDeleter CreateDeleter();
    }
}