using Db4objects.Db4o;

namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public class Db4oConnection : IDb4oConnection
    {
        private readonly bool _isDisposed;
        private readonly IObjectContainer _db4oObjectContainer;

        public Db4oConnection(IObjectContainer objectContainer)
        {
            _db4oObjectContainer = objectContainer;
            _isDisposed = false;
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _db4oObjectContainer.Close();
                _db4oObjectContainer.Dispose();
            }

        }
    }
}