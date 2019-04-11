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
        /// <returns>The connection string</returns>
        string GetConnectionString();
    }
}
