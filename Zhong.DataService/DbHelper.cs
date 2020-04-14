using System.Data;

namespace Zhong.DataService
{
    public class DbHelper
    {
        public readonly DbFactory Factory;
        public DbHelper(string sqlType, string connectionString)
        {
            Factory = new DbFactory(sqlType, connectionString);
        }
        /// <summary>
        /// 执行sql语句，返回影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql)
        {
            Factory.Command.Connection = Factory.Connection;
            Factory.Command.CommandText = sql;
            try
            {
                Factory.Command.Connection.Open();
                int num = Factory.Command.ExecuteNonQuery();
                return num;
            }
            finally
            {
                if (Factory.Command.Connection.State != System.Data.ConnectionState.Closed)
                {
                    Factory.Command.Connection.Close();
                }            
            }
        }
        /// <summary>
        /// 执行sql语句，返回影响的行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="ignoreConnectionState">忽略连接状态，必须在调用本方法前检查连接已打开及调用后适当关闭或释放连接</param>
        /// <returns></returns>
        public int ExecuteNonQuery(string sql, bool ignoreConnectionState)
        {
            if (!ignoreConnectionState)
            {
                return ExecuteNonQuery(sql);
            }

            Factory.Command.Connection = Factory.Connection;
            Factory.Command.CommandText = sql;
            Factory.Command.CommandTimeout = 0;
            return Factory.Command.ExecuteNonQuery();
        }
        /// <summary>
        /// 执行查询，返回查询结果
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataSet ExecuteQuery(string sql)
        {
            Factory.Command.Connection = Factory.Connection;
            Factory.Command.CommandText = sql;
            try
            {
                Factory.Command.Connection.Open();
                Factory.Adapter.SelectCommand = Factory.Command;
                DataSet ds = new DataSet();
                Factory.Adapter.Fill(ds);
                return ds;
            }
            finally
            {
                if (Factory.Command.Connection.State != System.Data.ConnectionState.Closed)
                {
                    Factory.Command.Connection.Close();
                }
            }
        }
        /// <summary>
        /// 执行查询，返回查询结果
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="ignoreConnectionState"></param>
        /// <returns></returns>
        public DataSet ExecuteQuery(string sql, bool ignoreConnectionState)
        {
            if (!ignoreConnectionState)
            {
                return ExecuteQuery(sql);
            }

            Factory.Command.Connection = Factory.Connection;
            Factory.Command.CommandText = sql;
            Factory.Adapter.SelectCommand = Factory.Command;
            DataSet ds = new DataSet();
            Factory.Adapter.Fill(ds);
            return ds;
        }
    }
}
