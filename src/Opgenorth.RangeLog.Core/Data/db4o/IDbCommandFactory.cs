using Opgenorth.RangeLog.Core.Data;

namespace CanWest.IDAS.Data.Custom
{
    public interface IDbCommandFactory
    {
        IDbCommand CreateCommandFrom(IDbConnection connection, IQuery query);
    }
}