using System.Configuration;
using ApplicationFrameworkSDK.Interfaces;

namespace ApplicationFrameworkSDK.Data
{
    /// <summary>
    /// Provides connection string values from the connectionStrings section of the config file
    /// </summary>
    public class ConnectionStringsSectionProvider : IDBConnectionStringProvider
    {
        /// <summary>
        /// Get connection string
        /// </summary>
        /// <param name="connectionName">Connection Name</param>
        /// <returns><see cref="string"/> containing the connection string</returns>
        public string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}
