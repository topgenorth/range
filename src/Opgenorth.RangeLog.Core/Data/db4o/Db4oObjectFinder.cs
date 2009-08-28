using System;

using Db4objects.Db4o;

using Opgenorth.RangeLog.Core.Domain;


namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public class Db4oObjectFinder : IDb4oObjectFinder
    {
        private readonly IObjectContainer _db;


        private bool _isDisposed;


        public Db4oObjectFinder(IObjectContainer db)
        {
            _db = db;
            _isDisposed = false;
        }

        public T FetchObjectMatchingId<T>(Guid id) where T : class, IDomainObject
        {
            Predicate<T> findGuid = trip => trip.ID.Equals(id);

            var set = _db.Query(findGuid);
            if ((set == null) || (set.Count == 0))
            {
                return null;
            }
            return set[0];
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _db.Close();
                _db.Dispose();
                _isDisposed = true;
            }
        }
    }
}