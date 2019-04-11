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

        /// <summary>
        /// Factory method to create <see cref="DBContext"/> instance
        /// </summary>
        /// <param name="provider">DB Provider to use</param>
        /// <param name="CSProvider">Connection String provider to use</param>
        /// <returns><see cref="DBContext"/> instance</returns>
        public static IDBContext Create(DataProviders provider, IDBConnectionStringProvider CSProvider)
        {
            return new DBContext(provider, CSProvider);
        }

        /// <summary>
        /// Create <see cref="DbDataAdapter"/> instance
        /// </summary>
        /// <param name="command"><see cref="DbCommand"/> instance</param>
        /// <returns><see cref="DbDataAdapter"/> instance</returns>
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

        /// <summary>
        /// Create <see cref="DbCommand"/> instance
        /// </summary>
        /// <param name="commandText">Command Text</param>
        /// <param name="commandType">Command Type</param>
        /// <param name="parameters">Parameter List</param>
        /// <param name="connection"><see cref="DbConnection"/></param>
        /// <returns><see cref="DbCommand"/> instance</returns>
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

        /// <summary>
        /// Create <see cref="DbConnection"/> instance
        /// </summary>
        /// <returns><see cref="DbConnection"/> instance</returns>
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

        /// <summary>
        /// Create <see cref="DbDataReader"/> instance
        /// </summary>
        /// <param name="command"><see cref="DbCommand"/></param>
        /// <returns><see cref="DbDataReader"/> instance</returns>
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

        /// <summary>
        /// Create <see cref="IDbDataParameter"/>
        /// </summary>
        /// <param name="type">Parameter type</param>
        /// <param name="direction">Direction</param>
        /// <param name="name">Parameter Name</param>
        /// <param name="value">Parameter value</param>
        /// <returns><see cref="IDbDataParameter"/></returns>
        public IDbDataParameter CreateParameter(DbType type, ParameterDirection direction, string name, object value)
        {
            return CreateParameter(type, direction, name, null, null, null, null, DataRowVersion.Proposed, value);
        }

        /// <summary>
        /// Create <see cref="IDbDataParameter"/>
        /// </summary>
        /// <param name="type">Parameter type</param>
        /// <param name="direction">Direction</param>
        /// <param name="name">Parameter Name</param>
        /// <param name="precision">Value precision</param>
        /// <param name="scale">Value scale</param>
        /// <param name="size">Value size</param>
        /// <param name="sourceColumn">Source column</param>
        /// <param name="version">Row version</param>
        /// <param name="value">Value</param>
        /// <returns><see cref="IDbDataParameter"/></returns>
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
