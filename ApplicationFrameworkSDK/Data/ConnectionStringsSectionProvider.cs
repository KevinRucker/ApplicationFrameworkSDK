using System.Configuration;
using ApplicationFrameworkSDK.Interfaces;

namespace ApplicationFrameworkSDK.Data
{
    public class ConnectionStringsSectionProvider : IDBConnectionStringProvider
    {
        public string GetConnectionString(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }
    }
}
