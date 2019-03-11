using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataAccessLayer
{
    internal static class ProviderManager
    {
        public static DbProviderFactory GetDbProviderFactory(string connectionName)
        {
            return DbProviderFactories.GetFactory(ConfigurationSettings.GetProviderName(connectionName));
        }
    }
}
