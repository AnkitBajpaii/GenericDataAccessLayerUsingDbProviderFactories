using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenericDataAccessLayer
{
    /// <summary>
    /// Wrapper class to create the crud functions to return ado.net objects like DataTable, DataSet, DataReader, ScalarValues etc
    /// </summary>
    public class DatabaseManager
    {
        readonly DatabaseHelper databaseHelper;

        public DatabaseManager(string connectionName)
        {
            this.databaseHelper = new DatabaseHelper(connectionName);
        }

        private IDbConnection CreateConnection()
        {
            return this.databaseHelper.CreateConnection();
        }

        private IDbCommand CreateCommand(string cmdText, CommandType cmdType, IDbConnection connection, IDbDataParameter[] parameters = null)
        {
            return this.databaseHelper.CreateCommand(cmdText, cmdType, connection, parameters);
        }

        public int ExecuteNonQuery(string cmdText, CommandType cmdType, IDbDataParameter[] parameters = null)
        {
            int i = 0;
            using (var conn = CreateConnection())
            {
                var cmd = CreateCommand(cmdText, cmdType, conn, parameters);
                cmd.CommandType = cmdType;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                conn.Open();
                i = cmd.ExecuteNonQuery();
            }

            return i;
        }

        public object ExecuteScalar(string cmdText, CommandType cmdType, IDbDataParameter[] parameters = null)
        {
            object obj = null;
            using (var conn = CreateConnection())
            {
                var cmd = CreateCommand(cmdText, cmdType, conn, parameters);
                cmd.CommandType = cmdType;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                conn.Open();
                obj = cmd.ExecuteScalar();
            }

            return obj;
        }

        public IDataReader GetDataReader(string cmdText, CommandType cmdType, IDbDataParameter[] parameters = null)
        {
            IDataReader dr = null;

            using (var conn = this.CreateConnection())
            {
                var cmd = this.CreateCommand(cmdText, cmdType, conn, parameters);
                cmd.CommandType = cmdType;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                conn.Open();
                dr = cmd.ExecuteReader();
            }

            return dr;
        }

        public DataSet GetDataSet(string cmdText, CommandType cmdType, IDbDataParameter[] parameters = null)
        {
            DataSet ds = null;

            using (var conn = CreateConnection())
            {
                var cmd = CreateCommand(cmdText, cmdType, conn, parameters);
                cmd.CommandType = cmdType;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                var da = this.databaseHelper.CreateAdapter(cmd);
                da.Fill(ds);
            }

            return ds;
        }

        public DataTable GetDataTable(string cmdText, CommandType cmdType, IDbDataParameter[] parameters = null)
        {
            DataSet ds = null;

            using (var conn = this.CreateConnection())
            {
                var cmd = this.CreateCommand(cmdText, cmdType, conn, parameters);
                cmd.CommandType = cmdType;

                if (parameters != null)
                {
                    foreach (var parameter in parameters)
                    {
                        cmd.Parameters.Add(parameter);
                    }
                }

                var da = this.databaseHelper.CreateAdapter(cmd);
                da.Fill(ds);
            }

            return ds?.Tables[0];
        }
    }
}
