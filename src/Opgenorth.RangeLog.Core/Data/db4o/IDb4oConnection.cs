using System;
using Db4objects.Db4o.Query;

namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public interface IDb4oConnection : IDisposable
    {
        ICommand CreateCommandToExecute(IQuery query);

//        IDbCommand CreateCommandToExecute(IQuery query);
    }
}