using System.Collections.Generic;
using Db4objects.Db4o.Query;

namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public interface IDb4oGateway
    {
        void DeleteUsingA(object builder);

        IEnumerable<object> GetASetOfObjectsUsingA(IQuery query);

        object GetASingleObjectUsingA(IQuery query);

        int InsertDataUsingA(object builder);

        void UpdateUsingA(object builder);

        void Execute(ICommand command);
    }
}