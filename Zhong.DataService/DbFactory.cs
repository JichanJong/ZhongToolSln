using System;
using System.Data.Common;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Oracle.ManagedDataAccess.Client;

namespace Zhong.DataService
{
    public class DbFactory
    {
        public DbConnection Connection { get; private set; }
        public DbCommand Command { get; private set; }
        public DbDataAdapter Adapter { get; private set; }

        public DbFactory(string sqlType,string connectionString)
        {
            sqlType = (sqlType ?? "").ToLower();
            switch (sqlType)
            {
                case "sqlserver":
                    Connection = new SqlConnection(connectionString);
                    Command = new SqlCommand();
                    Adapter = new SqlDataAdapter();
                    break;
                case "mysql":
                    Connection = new MySqlConnection(connectionString);
                    Command = new MySqlCommand();
                    Adapter = new MySqlDataAdapter();
                    break;
                case "oracle":
                    Connection = new OracleConnection(connectionString);
                    Command = new OracleCommand();
                    Adapter = new OracleDataAdapter();
                    break;
                default:
                    throw  new NotImplementedException("未实现该类型数据库的操作逻辑");
                    break;
            }
        }
    }
}
