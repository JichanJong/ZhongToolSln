using System.Data;
using System.Data.SqlClient;

namespace Zhong.DataService
{
    public class SqlDbHelper : IDbHelper
    {
        private readonly string _connectionString;
        public SqlDbHelper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public int ExecuteNonQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }


        public DataSet ExecuteQuery(string sql)
        {
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        conn.Open();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public DataSet GetTables(string key)
        {
            string sql =
                @"SELECT O.name AS [TableName],P.value AS [Description] FROM sys.objects O LEFT JOIN sys.extended_properties P ON O.object_id = P.major_id AND P.class = 1 AND P.minor_id = 0 AND P.name = 'MS_Description' WHERE type = 'U' and (O.name like @Key or cast(P.value as nvarchar(4000)) like @Key) ORDER BY O.name";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.Add(new SqlParameter("Key", "%"+ key + "%"));
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        conn.Open();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }
        }

        public DataSet GetColumns(string tableName)
        {
            string sql = $@"SELECT col.name AS ColumnName ,
                ISNULL(ep.[value], '') AS [Description] ,
                t.name AS DataType ,
                col.length AS [DataLength],
                ISNULL(COLUMNPROPERTY(col.id, col.name, 'Scale'), 0) AS [Precision] ,
                COLUMNPROPERTY(col.id, col.name, 'IsIdentity') as [Identity],
                CASE WHEN EXISTS ( SELECT 1
                FROM dbo.sysindexes si
                INNER JOIN dbo.sysindexkeys sik ON si.id = sik.id
                AND si.indid = sik.indid
                INNER JOIN dbo.syscolumns sc ON sc.id = sik.id
                AND sc.colid = sik.colid
                INNER JOIN dbo.sysobjects so ON so.name = si.name
                AND so.xtype = 'PK'
                WHERE sc.id = col.id
                AND sc.colid = col.colid ) THEN 1
                ELSE 0
                END AS [PrimaryKey] ,
                col.isnullable as IsNullable,
                ISNULL(comm.text, '') AS [DefaultValue]
                FROM dbo.syscolumns col
                LEFT JOIN dbo.systypes t ON col.xtype = t.xusertype
                inner JOIN dbo.sysobjects obj ON col.id = obj.id
                AND obj.xtype = 'U'
                AND obj.status >= 0
                LEFT JOIN dbo.syscomments comm ON col.cdefault = comm.id
                LEFT JOIN sys.extended_properties ep ON col.id = ep.major_id
                AND col.colid = ep.minor_id
                AND ep.name = 'MS_Description'
                LEFT JOIN sys.extended_properties epTwo ON obj.id = epTwo.major_id
                AND epTwo.minor_id = 0
                AND epTwo.name = 'MS_Description'
                WHERE obj.name = '{tableName}'
                ORDER BY col.colorder ;";
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataSet ds = new DataSet();
                        conn.Open();
                        adapter.Fill(ds);
                        return ds;
                    }
                }
            }

        }
    }
}