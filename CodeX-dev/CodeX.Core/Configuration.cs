using System.Configuration;

namespace CodeX.Configuration
{
    /// <summary>
    /// 
    /// </summary>
    public static class Configuration
    {
        /// <summary>
        /// Gets the app settings value.
        /// </summary>
        /// <param name="settingsKey">The settings key.</param>
        /// <returns>Value of the setting</returns>
        /// <example><code>
        /// var logPath = "LogFilePath".GetSettings();
        /// </code></example>
        public static string GetSettings(this string settingsKey)
        {
            return ConfigurationManager.AppSettings[settingsKey]??string.Empty;
        }

        /// <summary>
        /// Gets the connection string.
        /// </summary>
        /// <param name="configurationName">Name of the configuration.</param>
        /// <returns>Connection String</returns>
        /// <example><code>
        /// var connectionString = "DataConnection".GetConnectionString();
        /// </code></example>
        public static string GetConnectionString(this string configurationName)
        {
               return ConfigurationManager.ConnectionStrings[configurationName] == null
                           ? string.Empty
                           : ConfigurationManager.ConnectionStrings[configurationName].ConnectionString;
           
        }
    }
}
