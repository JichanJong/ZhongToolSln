using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Zhong.DataService;
using ZhongTool.Common;
using ZhongTool.Model;

namespace ZhongTool
{
    /// <summary>
    /// FrmSyncMultDatabase.xaml 的交互逻辑
    /// </summary>
    public partial class FrmSyncMultDatabase : Window
    {
        private readonly string connectionFile = "Connections.json";

        public FrmSyncMultDatabase()
        {
            InitializeComponent();
        }

        private void FrmSyncMultDatabase_Load(object sender, RoutedEventArgs e)
        {
            LoadAndBindData();
        }

        private void LoadAndBindData()
        {
            List<ConnectionInfo> lstData = JsonHelper.DeserializeObject<List<ConnectionInfo>>(connectionFile);
            lstConnection.ItemsSource = lstData;
        }

        private void btnExecute_Click(object sender, RoutedEventArgs e)
        {
            var conns = lstConnection.SelectedItems;
            if(conns.Count > 0)
            {
                foreach (var conn in conns)
                {
                    var con = (ConnectionInfo)conn;
                    ExecuteSql(con.ConnectionString);
                }
                MessageBox.Show("ok");
            }
        }

        private void ExecuteSql(string connectionString)
        {
            string sql = txtSql.Text;
            if (!string.IsNullOrWhiteSpace(sql))
            {
                var helper = new SqlDbHelper(connectionString);
                string[] lines = sql.Split(new[] { "GO\r\n","go\r\n"},StringSplitOptions.None);
                foreach (string line in lines)
                {
                    if(!string.IsNullOrWhiteSpace(line))
                    {
                        helper.ExecuteNonQuery(line,false);
                    }
                }
                helper.Dispose();
            }
        }
    }
}
