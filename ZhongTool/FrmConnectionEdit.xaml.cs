using System.Collections.Generic;
using System.Linq;
using System.Windows;
using ZhongTool.Common;
using ZhongTool.Model;

namespace ZhongTool
{
    /// <summary>
    /// FrmConnectionEdit.xaml 的交互逻辑
    /// </summary>
    public partial class FrmConnectionEdit : Window
    {
        public int Id { get; set; }
        private List<ConnectionInfo> LstConnectionInfos { get; set; }

        private readonly string connectionFile = "Connections.json"; 
        public FrmConnectionEdit()
        {
            InitializeComponent();
        }

        private void FrmConnectionEdit_Loaded(object sender, RoutedEventArgs e)
        {
            List<BindEnumItem> lstData = EnumHelper.EnumToList<DataBaseType>();
            cmbDatabaseType.ItemsSource = lstData;
            LstConnectionInfos = JsonHelper.DeserializeObject<List<ConnectionInfo>>(connectionFile) ?? new List<ConnectionInfo>();

            if (Id != 0)
            {
                var info = LstConnectionInfos.SingleOrDefault(l => l.Id == Id);
                if (info == null)
                {
                    MessageBox.Show("该连接已被删除");
                    return;
                }

                txtName.Text = info.Name;
                txtConnectionString.Text = info.ConnectionString;
                cmbDatabaseType.SelectedValue = (int)info.DatabaseType;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if (Id == 0)
            {
                //添加
                int id;
                if (LstConnectionInfos.Count > 0)
                {
                    id = LstConnectionInfos.Max(c => c.Id) + 1;
                }
                else
                {
                    id = 1;
                }
                ConnectionInfo info = new ConnectionInfo { Id = id, ConnectionString = txtConnectionString.Text.Trim(), Name = txtName.Text.Trim(), DatabaseType = (DataBaseType)cmbDatabaseType.SelectedValue };
                LstConnectionInfos.Add(info);
                JsonHelper.SerializeObject(LstConnectionInfos, connectionFile);
                MessageBox.Show("添加成功");
            }
            else
            {

                ConnectionInfo info = LstConnectionInfos?.Single(l => l.Id == Id);
                if (info != null)
                {
                    info.Name = txtName.Text.Trim();
                    info.ConnectionString = txtConnectionString.Text.Trim();
                    info.DatabaseType = (DataBaseType) cmbDatabaseType.SelectedValue;
                    JsonHelper.SerializeObject(LstConnectionInfos, connectionFile);
                    MessageBox.Show("保存成功");
                }          
            }

            DialogResult = true;
        }
    }
}
