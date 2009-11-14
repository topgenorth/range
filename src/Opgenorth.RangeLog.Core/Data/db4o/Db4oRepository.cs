using System;
using System.Collections.Generic;
using System.Linq;

using Opgenorth.RangeLog.Core.Domain;


namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public interface IDb4oTripRepository : IRepository<ITrip>
    {
    }

    public class Db4oRepository : IDb4oTripRepository
    {
        private readonly IDb4oConnectionFactory _connectionFactory;


        public Db4oRepository(IDb4oConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Add(ITrip entity)
        {
            using (var db = _connectionFactory.CreateObjectContainer())
            {
                db.Store(entity);
            }
        }

        public void Remove(ITrip entity)
        {
            using (var deleter = _connectionFactory.CreateDeleter())
            {
                deleter.DeleteObject(entity);
            }
        }

        public void Update(ITrip trip)
        {
            using (var db = _connectionFactory.CreateObjectContainer())
            {
                db.Store(trip);
            }
        }

        public ITrip FetchById(Guid id)
        {
            ITrip trip;
            using (var fetcher = _connectionFactory.CreateFinder())
            {
                trip = fetcher.FetchObjectMatchingId<ITrip>(id);
            }
            return trip;
        }

        public IEnumerable<ITrip> FetchAll()
        {
            IEnumerable<ITrip> trips = null;
            using (var db = _connectionFactory.CreateObjectContainer())
            {
                var list = db.Query<ITrip>();
                trips = list.ToArray();
            }

            return trips ?? new ITrip[0];
        }
    }
}