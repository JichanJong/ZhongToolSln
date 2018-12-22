using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Win32;
using Zhong.DataService;
using ZhongTool.Model;

namespace ZhongTool
{
    /// <summary>
    /// FrmExecuteSql.xaml 的交互逻辑
    /// </summary>
    public partial class FrmExecuteSql : Window
    {
        public DbHelper DbHelper { get; set; }
        public ConnectionInfo ConnInfo { get; set; }

        public FrmExecuteSql()
        {
            InitializeComponent();
        }

        private void BtnConnectionManagement_Click(object sender, RoutedEventArgs e)
        {
            FrmConnectionManagement frm = new FrmConnectionManagement {ParentWindow = this};
            if (frm.ShowDialog() == true)
            {
                lblConnectionInfo.Text = $"{ConnInfo.Name}, {ConnInfo.DatabaseType.ToString()}";
                SetControlState();
            }
        }

        private void SetControlState()
        {
            btnDisconnect.IsEnabled = (DbHelper != null);
        }

        private void BtnDisconnect_Click(object sender, RoutedEventArgs e)
        {
            DbHelper = null;
            lblConnectionInfo.Text = "";
            SetControlState();
        }

        private void BtnExecuteSql_Click(object sender, RoutedEventArgs e)
        {
            if (DbHelper == null)
            {
                MessageBox.Show("请先连接数据库再进行此操作");
                return;
            }

            string sql = rtbSqlCode.Selection.Text;
            if (string.IsNullOrWhiteSpace(sql))
            {
                sql = new TextRange(rtbSqlCode.Document.ContentStart, rtbSqlCode.Document.ContentEnd).Text;
            }
            ExecuteSql(sql);
        }

        private void ExecuteSql(string sql)
        {
            if (!string.IsNullOrWhiteSpace(sql))
            {
                if (chkContinue.IsChecked == true)
                {
                    try
                    {
                        int num = DbHelper.ExecuteNonQuery(sql, true);
                        txtResult.Text += $"command execute successfully,{num} rows effect {Environment.NewLine}";
                    }
                    catch (Exception ex)
                    {
                        txtResult.Text +=
                            $"error occurred when execute command,the error info is {ex.ToString()} {Environment.NewLine}";
                    }
                }
                else
                {
                    int num = DbHelper.ExecuteNonQuery(sql, true);
                    txtResult.Text += $"command execute successfully,{num} rows effect {Environment.NewLine}";
                }
            }
        }

        private void BtnExecuteSqlFile_Click(object sender, RoutedEventArgs e)
        {
            if (DbHelper == null)
            {
                MessageBox.Show("请先连接数据库再进行此操作");
                return;
            }
            OpenFileDialog ofd = new OpenFileDialog {Filter = "SQL文件|*.sql|文本文件|*.txt"};
            if (ofd.ShowDialog() == true)
            {
                string path = ofd.FileName;
                using (FileStream fs =new FileStream(path,FileMode.Open,FileAccess.Read))
                {
                    using (StreamReader reader = new StreamReader(fs))
                    {
                        StringBuilder sb =new StringBuilder();
                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            if (line.Trim().Equals("go", StringComparison.OrdinalIgnoreCase))
                            {
                                if (sb.Length > 0)
                                {
                                    ExecuteSql(sb.ToString());
                                    sb.Length = 0;
                                }
                            }
                            else
                            {
                                sb.AppendLine(line);
                            }
                        }
                    }
                }

                MessageBox.Show("执行完毕");
            }
        }
    }
}
