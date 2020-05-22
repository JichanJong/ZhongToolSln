using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ZhongTool
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MD5MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrmMD5 frm = new FrmMD5();
            frm.Show();
        }

        private void Base64MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrmBase64 frm = new FrmBase64();
            frm.Show();
        }

        private void DESMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrmDES frm = new FrmDES();
            frm.Show();
        }

        private void URLMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrmURLEncodeDecode frm = new FrmURLEncodeDecode();
            frm.Show();
        }

        private void HTMLMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrmHtmlEncodeDecode frm = new FrmHtmlEncodeDecode();
            frm.Show();
        }

        private void ExecuteSqlMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrmExecuteSql frm = new FrmExecuteSql();
            frm.ShowDialog();
        }

        private void GuidMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            FrmGuid frm = new FrmGuid();
            frm.Show();
        }

        private void QrcodeMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            FrmQrcode frm = new FrmQrcode();
            frm.Show();
        }

        private void regex_Click(object sender, RoutedEventArgs e)
        {
            FrmRegex frm = new FrmRegex();
            frm.Show();
        }

        private void DatabaseTool_Click(object sender, RoutedEventArgs e)
        {
            FrmDatabaseTool frm = new FrmDatabaseTool();
            frm.ShowDialog();
        }

        private void JsonTool_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void AndroidGenerateCode_Click(object sender, RoutedEventArgs e)
        {
            FrmAndroidTool frm = new FrmAndroidTool();
            frm.Show();
        }

        private void FormatVariable_Click(object sender, RoutedEventArgs e)
        {
            FrmFormatVariable frm = new FrmFormatVariable();
            frm.Show();
        }

        private void getSqlPara_Click(object sender, RoutedEventArgs e)
        {
            FrmGetSqlPara frm = new FrmGetSqlPara();
            frm.Show();
        }

        private void formatSqlInPara_Click(object sender, RoutedEventArgs e)
        {
            FrmFormatSqlInPara frm = new FrmFormatSqlInPara();
            frm.Show();
        }
    }
}
