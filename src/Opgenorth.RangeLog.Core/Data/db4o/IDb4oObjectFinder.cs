using System;

using Opgenorth.RangeLog.Core.Domain;


namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public interface IDb4oObjectFinder : IDb4oObjectCommand
    {
        T FetchObjectMatchingId<T>(Guid id) where T : class, IDomainObject;
    }
}