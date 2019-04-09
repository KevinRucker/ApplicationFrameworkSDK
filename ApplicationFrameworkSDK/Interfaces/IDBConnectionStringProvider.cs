namespace ApplicationFrameworkSDK.Interfaces
{
    /// <summary>
    /// Interface for classes implementing ConnectionString provider functionality
    /// </summary>
    public interface IDBConnectionStringProvider
    {
        /// <summary>
        /// Retrieve the connection string
        /// </summary>
        /// <param name="connectionName">Named connection to get connection string for</param>
        /// <returns>The connection string</returns>
        string GetConnectionString(string connectionName);
    }
}
