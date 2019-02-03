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
            frm.ShowDialog();
        }

        private void Base64MenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrmBase64 frm = new FrmBase64();
            frm.ShowDialog();
        }

        private void DESMenuItem_Click(object sender, RoutedEventArgs e)
        {
        }

        private void URLMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrmURLEncodeDecode frm = new FrmURLEncodeDecode();
            frm.ShowDialog();
        }

        private void HTMLMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrmHtmlEncodeDecode frm = new FrmHtmlEncodeDecode();
            frm.ShowDialog();
        }

        private void ExecuteSqlMenuItem_Click(object sender, RoutedEventArgs e)
        {
            FrmExecuteSql frm = new FrmExecuteSql();
            frm.ShowDialog();
        }

        private void GuidMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            FrmGuid frm = new FrmGuid();
            frm.ShowDialog();
        }

        private void QrcodeMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            FrmQrcode frm = new FrmQrcode();
            frm.ShowDialog();
        }

        private void regex_Click(object sender, RoutedEventArgs e)
        {
            FrmRegex frm = new FrmRegex();
            frm.ShowDialog();
        }
    }
}
