using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
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
    /// FrmDatabaseTool.xaml 的交互逻辑
    /// </summary>
    public partial class FrmDatabaseTool : Window
    {
        private readonly string connectionFile = "Connections.json";
        private IDbHelper dbHelper;
        public FrmDatabaseTool()
        {
            InitializeComponent();
        }

        private void btnAddConn_Click(object sender, RoutedEventArgs e)
        {
            FrmConnectionEdit frm = new FrmConnectionEdit();
            if (frm.ShowDialog() == true)
            {
                LoadConnections();
            }
        }

        private void LoadConnections()
        {
            List<ConnectionInfo> lstData = JsonHelper.DeserializeObject<List<ConnectionInfo>>(connectionFile);
            cmbConnections.ItemsSource = lstData;
            cmbConnections.SelectedIndex = 0;
        }

        private void FrmDatabaseTool_Loaded(object sender, RoutedEventArgs e)
        {
            LoadConnections();
        }

        private void BtnQuery_Click(object sender, RoutedEventArgs e)
        {
            ConnectionInfo info = cmbConnections.SelectedItem as ConnectionInfo;
            if (info == null)
            {
                MessageBox.Show("请选择连接");
                return;
            }

            string key = txtKey.Text.Trim();
            
            if (info.DatabaseType == DataBaseType.SqlServer)
            {
                dbHelper = new SqlDbHelper(info.ConnectionString);
            }

            if (dbHelper != null)
            {
                DataTable dt = dbHelper.GetTables(key).Tables[0];
                gridTable.DataContext = dt.DefaultView;
            }
        }

        private void TableRow_DoubleClick(object sender, MouseButtonEventArgs e)
        {
            DataGridRow row = sender as DataGridRow;
            if (row != null)
            {
                DataRowView drv = (DataRowView) row.Item;
                string tableName = drv["TableName"].ToString();
                if (dbHelper != null)
                {
                    DataTable dt = dbHelper.GetColumns(tableName).Tables[0];
                    gridColumn.DataContext = dt.DefaultView;
                }
            }
        }
    }
}
