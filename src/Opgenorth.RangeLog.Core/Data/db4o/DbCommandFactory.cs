
using Opgenorth.RangeLog.Core.Data;

namespace CanWest.IDAS.Data.Custom
{
    public class DbCommandFactory : IDbCommandFactory
    {
        public IDbCommand CreateCommandFrom(IDbConnection connection, IQuery query)
        {
            IDbCommand cmd = connection.CreateCommand();
            query.Prepare(cmd);
            return cmd;
        }
    }
}