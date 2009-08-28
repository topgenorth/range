using System;
using System.Collections.Generic;
using Db4objects.Db4o.Query;

namespace Opgenorth.RangeLog.Core.Data.db4o
{
    public class Db4oGateway : IDb4oGateway
    {
        private IDb4oConnectionFactory _connectionFactory;

        public Db4oGateway(IDb4oConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public void DeleteUsingA(object builder)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<object> GetASetOfObjectsUsingA(IQuery query)
        {
            throw new NotImplementedException();
        }

        public object GetASingleObjectUsingA(IQuery query)
        {
            throw new NotImplementedException();
        }

        public int InsertDataUsingA(object builder)
        {
            throw new NotImplementedException();
        }

        public void UpdateUsingA(object builder)
        {
            throw new NotImplementedException();
        }

        public void Execute(ICommand command)
        {
            throw new NotImplementedException();
        }

//        public Db4oGateway(IDb4oConnectionFactory connectionFactory)
//        {
//            _connectionFactory = connectionFactory;
//        }
//
//
//        public void DeleteUsingA(IDeleteStatementBuilder builder)
//        {
//            using (var connection = _connectionFactory.Create())
//            {
//                using (var command = connection.CreateCommandToExecute(builder))
//                {
//                    command.ExecuteNonQuery();
//                }
//            }
//        }
//
//        public void Execute(IStoredProcedure storedProc)
//        {
//            using (var connection = _connectionFactory.Create())
//            {
//                using (var command = connection.CreateCommandToExecute(storedProc))
//                {
//                    command.ExecuteNonQuery();
//                }
//            }
//        }
//
//        public DataTable GetADatatableUsingA(IQuery query)
//        {
//            using (var connection = _connectionFactory.Create())
//            {
//                using (var command = connection.CreateCommandToExecute(query))
//                {
//                    using (var reader = command.ExecuteReader())
//                    {
//                        var results = new DataTable();
//                        results.Load(reader);
//                        return results;
//                    }
//                }
//            }
//        }
//
//        public IEnumerable<DataRow> GetASetOfObjectsUsingA(IQuery query)
//        {
//            foreach (DataRow row in GetADatatableUsingA(query).Rows)
//            {
//                yield return row;
//            }
//        }
//
//        public DataRow GetASingleObjectUsingA(IQuery query)
//        {
//            using (var table = GetADatatableUsingA(query))
//            {
//                if (table.Rows.Count > 0)
//                {
//                    return table.Rows[0];
//                }
//                else
//                {
//                    return null;
//                }
//            }
//        }
//
//        public int InsertDataUsingA(IInsertStatementBuilder builder)
//        {
//            using (var connection = _connectionFactory.Create())
//            {
//                using (var command = connection.CreateCommandToExecute(builder))
//                {
//                    var rowsAffected = Convert.ToInt32(command.ExecuteScalar());
//                    // [TO080206@1336] The first parameter of an insert sproc has to be the identity column.
//                    var identityColumn = ((IDataParameter) command.Parameters[0]);
//                    var insertedId = Convert.ToInt32(identityColumn.Value);
//                    return insertedId;
//                }
//            }
//        }
//
//        public void UpdateUsingA(IUpdateStatementBuilder builder)
//        {
//            using (var connection = _connectionFactory.Create())
//            {
//                using (var command = connection.CreateCommandToExecute(builder))
//                {
//                    var rowsAffected = command.ExecuteNonQuery();
//                }
//            }
//        }
    }
}