using System.Collections.Generic;
using System.Data;

namespace ApplicationFrameworkSDK.Interfaces
{
    public interface IDBContext
    {
        IDbConnection CreateConnection();
        IDbCommand CreateCommand(string commandText, CommandType commandType, List<IDbDataParameter> parameters, IDbConnection connection);
        IDbDataParameter CreateParameter(DbType type, ParameterDirection direction, string name, object value);
        IDbDataParameter CreateParameter(DbType type, ParameterDirection direction, string name, byte? precision, byte? scale, int? size, string sourceColumn, DataRowVersion version, object value);
        IDbDataAdapter CreateAdapter(IDbCommand command);
        IDataReader CreateDataReader(IDbCommand command);
    }
}
