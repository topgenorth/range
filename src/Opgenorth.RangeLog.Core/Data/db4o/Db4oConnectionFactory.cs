using System;

using Db4objects.Db4o;


namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public class Db4oConnectionFactory : IDb4oConnectionFactory
    {
        private readonly IDb4oConfiguration _dbConfiguration;


        public IDb4oConfiguration Configuration
        {
            get { return _dbConfiguration; }
        }

        public Db4oConnectionFactory(IDb4oConfiguration _dbConfiguration)
        {
            if (_dbConfiguration == null)
            {
                throw new NullReferenceException("Must provide a valid IDb4oConfiguration.");
            }
            this._dbConfiguration = _dbConfiguration;
        }

        public IObjectContainer CreateObjectContainer()
        {
            var objectContainer = Db4oFactory.OpenFile(_dbConfiguration.GetFilename());
            return objectContainer;
        }

        public IDb4oObjectDeleter CreateDeleter()
        {
            var db = CreateObjectContainer();
            IDb4oObjectDeleter deleter = new Db4oObjectDeleter(db);
            return deleter;
        }

        public IDb4oObjectFinder CreateFinder()
        {
            var db = CreateObjectContainer();
            IDb4oObjectFinder finder = new Db4oObjectFinder(db);
            return finder;
        }
    }
}