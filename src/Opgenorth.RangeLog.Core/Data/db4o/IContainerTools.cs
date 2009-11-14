using System;
using Db4objects.Db4o;

namespace Opgenorth.RangeLog.Core.Data.db4o
{
    [Obsolete]
    public interface IContainerTools
    {
        string ContainerName { get; }
        IObjectContainer GetDb4oContainer();
        void CreateDataDirectory();
    }
}