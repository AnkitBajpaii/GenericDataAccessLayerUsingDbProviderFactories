using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataAccessLayer
{
    /// <summary>
    /// Wrapper class for creating ADO.NET Objects
    /// </summary>
    internal class DatabaseHelper
    {
        readonly string connectionString;
        readonly string connectionName;

        public DatabaseHelper(string connectionName)
        {
            this.connectionName = connectionName;
            this.connectionString = ConfigurationSettings.GetConnectionString(connectionName);
        }

        public void CloseConnection(IDbConnection connection)
        {
            connection.Close();
        }

        private  DbProviderFactory GetProviderFactory()
        {
            return ProviderManager.
                GetDbProviderFactory(this.connectionName);
        }

        public IDbConnection CreateConnection()
        {
            DbConnection conn = null;
            
            try
            {
                conn = GetProviderFactory().CreateConnection();
                conn.ConnectionString = this.connectionString;
                conn.Open();
            }
            catch (Exception)
            {
                //log
            }

            return conn;
        }

        public IDbDataAdapter CreateAdapter(IDbCommand command)
        {
            DbDataAdapter da = null;
            try
            {
                da = GetProviderFactory().CreateDataAdapter();
                da.SelectCommand = command as DbCommand;
            }
            catch (Exception)
            {
                //log
            }
            return da;
        }

        public IDbCommand CreateCommand(string cmdText, CommandType cmdType, IDbConnection connection, IDbDataParameter[] parameters = null)
        {
            DbCommand cmd = null;

            try
            {
                cmd = GetProviderFactory().CreateCommand();
                cmd.CommandText = cmdText;
                cmd.CommandType = cmdType;
                cmd.Connection = connection as DbConnection;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }
            }
            catch (Exception)
            {
                //log
            }

            return cmd;
        }

        public IDbDataParameter CreateParameter(string name, object value, DbType dbType, int size = 0, ParameterDirection direction = ParameterDirection.Input)
        {
            DbParameter parameter = null;
            try
            {
                parameter = GetProviderFactory().CreateParameter();
                parameter.ParameterName = name;
                parameter.Value = value;
                parameter.DbType = dbType;
                parameter.Size = size;
                parameter.Direction = direction;
            }
            catch (Exception)
            {
                //log
            }

            return parameter;

        }

    }
}
