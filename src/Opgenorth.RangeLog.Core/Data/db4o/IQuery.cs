using Db4objects;
namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public interface IQuery<T>
    {
        void Prepare();
    }
}