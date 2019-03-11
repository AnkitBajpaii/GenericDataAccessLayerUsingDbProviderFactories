using GenericDataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataAccessLayer
{
    internal static class ConfigurationSettings
    {
        private static ConnectionStringSettings GetConnectionStringsSettings(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName];
        }

        public static string GetProviderName(string connectionName)
        {
            return GetConnectionStringsSettings(connectionName).ProviderName;
        }

        public static string GetConnectionString(string connectionName)
        {
            return GetConnectionStringsSettings(connectionName).ConnectionString;
        }

    }
}
