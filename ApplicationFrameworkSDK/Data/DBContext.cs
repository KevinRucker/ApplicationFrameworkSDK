using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using ApplicationFrameworkSDK.Interfaces;
using ApplicationFrameworkSDK.Common;
using ApplicationFrameworkSDK.CustomExceptions;

namespace ApplicationFrameworkSDK.Data
{
    public class DBContext : IDBContext
    {
        private DataProviders _provider;
        private IDBConnectionStringProvider _CSProvider;

        private DBContext(DataProviders provider, IDBConnectionStringProvider CSProvider)
        {
            _provider = provider;
            _CSProvider = CSProvider;
        }

        public static IDBContext Create(DataProviders provider, IDBConnectionStringProvider CSProvider)
        {
            return new DBContext(provider, CSProvider);
        }

        public IDbDataAdapter CreateAdapter(IDbCommand command)
        {
            try
            {
                var factory = DbProviderFactories.GetFactory(_provider.GetDescription());
                var adapter = factory.CreateDataAdapter();
                adapter.SelectCommand = (DbCommand)command;
                return adapter;
            }
            catch (Exception ex)
            {
                throw new DBException("DBContext::CreateAdapter unable to create DB Adapter object.", ex);
            }
        }

        public IDbCommand CreateCommand(string commandText, CommandType commandType, List<IDbDataParameter> parameters, IDbConnection connection)
        {
            try
            {
                var factory = DbProviderFactories.GetFactory(_provider.GetDescription());
                var command = factory.CreateCommand();
                command.CommandText = commandText;
                command.CommandType = commandType;
                foreach(var parameter in parameters) { command.Parameters.Add(parameter); }
                if (connection.State != ConnectionState.Open) { connection.Open(); }
                command.Connection = (DbConnection)connection;
                return command;
            }
            catch (Exception ex)
            {
                throw new DBException("DBContext::CreateCommand unable to create DB Command object.", ex);
            }
        }

        public IDbConnection CreateConnection()
        {
            try
            {
                var factory = DbProviderFactories.GetFactory(_provider.GetDescription());
                var connection = factory.CreateConnection();
                connection.ConnectionString = _CSProvider.GetConnectionString();
                connection.Open();
                return connection;
            }
            catch(Exception ex)
            {
                throw new DBException("DBContext::CreateConnection unable to create DB Connection object.", ex);
            }
        }

        public IDataReader CreateDataReader(IDbCommand command)
        {
            try
            {
                return command.ExecuteReader();
            }
            catch (Exception ex)
            {
                throw new DBException("DBContext::CreateDataReader unable to create DataReader.", ex);
            }
        }

        public IDbDataParameter CreateParameter(DbType type, ParameterDirection direction, string name, object value)
        {
            return CreateParameter(type, direction, name, null, null, null, null, DataRowVersion.Proposed, value);
        }

        public IDbDataParameter CreateParameter(DbType type, ParameterDirection direction, string name, byte? precision, byte? scale, int? size, string sourceColumn, DataRowVersion version, object value)
        {
            try
            {
                var factory = DbProviderFactories.GetFactory(_provider.GetDescription());
                var parameter = factory.CreateParameter();
                parameter.DbType = type;
                parameter.Direction = direction;
                parameter.ParameterName = name;
                if (precision != null) { parameter.Precision = precision.Value; }
                if (scale != null) { parameter.Scale = scale.Value; }
                if (size != null) { parameter.Size = size.Value; }
                if (!string.IsNullOrEmpty(sourceColumn)) { parameter.SourceColumn = sourceColumn; }
                parameter.SourceVersion = version;
                parameter.Value = value;
                return parameter;
            }
            catch (Exception ex)
            {
                throw new DBException("DBContext::CreateParameter unable to create DB Parameter object.", ex);
            }
        }
    }
}
