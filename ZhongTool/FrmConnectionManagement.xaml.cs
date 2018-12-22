using System.Collections.Generic;
using System.Windows;
using Zhong.DataService;
using ZhongTool.Common;
using ZhongTool.Model;

namespace ZhongTool
{
    /// <summary>
    /// FrmConnectionManagement.xaml 的交互逻辑
    /// </summary>
    public partial class FrmConnectionManagement : Window
    {
        private readonly string connectionFile = "Connections.json";

        /// <summary>
        /// 执行SQL窗体
        /// </summary>
        public FrmExecuteSql ParentWindow { get; set; }

        public FrmConnectionManagement()
        {
            InitializeComponent();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            FrmConnectionEdit frm = new FrmConnectionEdit();
            if (frm.ShowDialog() == true)
            {
                LoadAndBindData();
            }
        }

        private void FrmConnectionManagement_Loaded(object sender, RoutedEventArgs e)
        {
            LoadAndBindData();
        }

        private void LoadAndBindData()
        {
            List<ConnectionInfo> lstData = JsonHelper.DeserializeObject<List<ConnectionInfo>>(connectionFile);
            lstConnection.ItemsSource = lstData;
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var info = lstConnection.SelectedItem as ConnectionInfo;
            if (info == null)
            {
                MessageBox.Show("请选择一条数据进行删除");
            }
            else
            {
                if (MessageBox.Show("确定要删除吗？", "操作提示", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    List<ConnectionInfo> lstData = JsonHelper.DeserializeObject<List<ConnectionInfo>>(connectionFile);
                    lstData.RemoveAll(i => i.Id == info.Id);
                    JsonHelper.SerializeObject(lstData, connectionFile);
                    MessageBox.Show("删除成功");
                    LoadAndBindData();
                }
            }
        }

        private void BtnConnect_Click(object sender, RoutedEventArgs e)
        {
            var info = lstConnection.SelectedItem as ConnectionInfo;
            if (info == null)
            {
                MessageBox.Show("请选择指定的数据库连接信息进行连接");
            }
            else if (ParentWindow != null)
            {
                ParentWindow.ConnInfo = info;
                var oldHelper = ParentWindow.DbHelper;
                ParentWindow.DbHelper = new DbHelper(info.DatabaseType.ToString().ToLower(), info.ConnectionString);
                ParentWindow.DbHelper.Factory.Connection.Open();
                oldHelper?.Factory.Connection.Dispose();
                DialogResult = true;
            }
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            var info = lstConnection.SelectedItem as ConnectionInfo;
            if (info == null)
            {
                MessageBox.Show("请选择指定的数据库连接信息进行编辑");
            }
            else
            {
                FrmConnectionEdit frm = new FrmConnectionEdit {Id = info.Id};
                if (frm.ShowDialog() == true)
                {
                    LoadAndBindData();
                }
            }
        }
    }
}
