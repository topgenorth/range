using Opgenorth.RangeLog.Core.Domain;


namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public interface IDb4oObjectDeleter : IDb4oObjectCommand
    {
        void DeleteObject<T>(T entity) where T : class, IDomainObject;
    }
}