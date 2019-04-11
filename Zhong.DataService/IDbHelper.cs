using System.Data;

namespace Zhong.DataService
{
    public interface IDbHelper
    {

        int ExecuteNonQuery(string sql);

        DataSet ExecuteQuery(string sql);

        DataSet GetTables(string key);

        DataSet GetColumns(string tableName);
    }
}