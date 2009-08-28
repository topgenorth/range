using System;
using System.Collections.Generic;
using Opgenorth.RangeLog.Core.Domain;

namespace Opgenorth.RangeLog.Core.Data
{
    public interface IRepository<TDomainObject>
        where TDomainObject : IDomainObject
    {
        void Add(TDomainObject entity);
        void Remove(TDomainObject entity);
        void Update(TDomainObject trip);
//        IDomainObject Fetch(IQuery query);

        TDomainObject FetchById(Guid id);
        IEnumerable<TDomainObject> FetchAll();

    }
}