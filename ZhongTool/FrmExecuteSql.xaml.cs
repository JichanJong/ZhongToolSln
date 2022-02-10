using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using Microsoft.Win32;
using Zhong.DataService;
using ZhongTool.Model;
using System.Linq;

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
                bool isContinue = false;
                chkContinue.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            new Action(() =>
                            {
                                isContinue = chkContinue.IsChecked == true;
                            }));
                if (isContinue)
                {
                    try
                    {
                        int num = DbHelper.ExecuteNonQuery(sql, true);
                        txtResult.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            new Action(() =>
                            {
                                txtResult.Text += $"command execute successfully,{num} rows effect {Environment.NewLine}";
                            }));
                       
                    }
                    catch (Exception ex)
                    {
                        txtResult.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                            new Action(() =>
                            {
                               txtResult.Text +=
                            $"error occurred when execute command,the error info is {ex.ToString()} {Environment.NewLine}";
                            }));
                    }
                }
                else
                {
                    int num = DbHelper.ExecuteNonQuery(sql, true);
                    txtResult.Dispatcher.Invoke(System.Windows.Threading.DispatcherPriority.Normal,
                        new Action(() =>
                        {
                               txtResult.Text += $"command execute successfully,{num} rows effect {Environment.NewLine}";
                        }));
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
                Thread thread = new Thread(()=>{
                    ExecuteFile(path);
                });
                thread.Start();
            }
        }

        private void ExecuteFile(string path)
        {
            using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs,Encoding.UTF8))
                {
                    StringBuilder sb = new StringBuilder();
                    string line;
                    int i = 0;
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
                            i++;
                            sb.AppendLine(line);
                            if(i == 1000)
                            {
                                ExecuteSql(sb.ToString());
                                i = 0;
                                sb.Length = 0;
                            }
                        }
                    }
                    if(sb.Length > 0)
                    {
                        ExecuteSql(sb.ToString());
                    }
                }
            }
        }

        private void btnExecuteSqlDirectory_Click(object sender, RoutedEventArgs e)
        {
            if (DbHelper == null)
            {
                MessageBox.Show("请先连接数据库再进行此操作");
                return;
            }
            System.Windows.Forms.FolderBrowserDialog ofd = new System.Windows.Forms.FolderBrowserDialog();
            if (ofd.ShowDialog() ==  System.Windows.Forms.DialogResult.OK)
            {
                string path = ofd.SelectedPath;
                string[] files = Directory.GetFiles(path);
                files = files.OrderBy(f => Path.GetFileName(f)).ToArray();
                foreach (string file in files)
                {
                    ExecuteFile(file);
                }
                MessageBox.Show("ok");
            }
        }
    }
}
