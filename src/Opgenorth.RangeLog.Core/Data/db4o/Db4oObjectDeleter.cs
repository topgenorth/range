using System;

using Db4objects.Db4o;

using Opgenorth.RangeLog.Core.Domain;


namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public class Db4oObjectDeleter : IDb4oObjectDeleter
    {
        private bool _isDisposed;


        private readonly IObjectContainer _db;


        public Db4oObjectDeleter(IObjectContainer db)
        {
            _db = db;
            _isDisposed = false;
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

        public void DeleteObject<T>(T entity) where T : class, IDomainObject
        {
            Predicate<T> findGuid = trip => trip.ID.Equals(entity.ID);

            var set = _db.Query(findGuid);

            if ((set == null) || (set.Count == 0))
            {
                return;
            }
            var entityToDelete = set[0];
            _db.Delete(entityToDelete);
        }
    }
}