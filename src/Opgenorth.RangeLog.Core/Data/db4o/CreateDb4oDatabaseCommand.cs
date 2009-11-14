using System.IO;


namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public interface ICreateDb4oDatabase : ICommand
    {
    }

    public class CreateDb4oDatabaseCommand : ICreateDb4oDatabase
    {
        private readonly IDb4oConnectionFactory _connectionFactory;


        public CreateDb4oDatabaseCommand(IDb4oConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void Execute()
        {
            var fileInfo = new FileInfo(_connectionFactory.Configuration.GetFilename());

            if (!fileInfo.Directory.Exists)
            {
                fileInfo.Directory.Create();
            }

            if (!fileInfo.Exists)
            {
                var db = _connectionFactory.CreateObjectContainer();
                db.Dispose();
            }
        }
    }
}