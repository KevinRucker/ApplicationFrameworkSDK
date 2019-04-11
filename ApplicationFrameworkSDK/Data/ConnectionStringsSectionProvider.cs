using System.Configuration;
using ApplicationFrameworkSDK.Interfaces;

namespace ApplicationFrameworkSDK.Data
{
    /// <summary>
    /// Provides connection string values from the connectionStrings section of the config file
    /// </summary>
    public class ConnectionStringsSectionProvider : IDBConnectionStringProvider
    {
        private string _connectionName = string.Empty;

        private ConnectionStringsSectionProvider(string connectionName)
        {
            _connectionName = connectionName;
        }

        /// <summary>
        /// FActory method to create instance of ConnectionStringsSectionProvider
        /// </summary>
        /// <param name="connectionName"></param>
        /// <returns><see cref="ConnectionStringsSectionProvider"/></returns>
        public static IDBConnectionStringProvider Create(string connectionName)
        {
            return new ConnectionStringsSectionProvider(connectionName);
        }

        /// <summary>
        /// Get connection string
        /// </summary>
        /// <returns><see cref="string"/> containing the connection string</returns>
        public string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings[_connectionName].ConnectionString;
        }
    }
}
