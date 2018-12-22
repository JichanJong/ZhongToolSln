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
using System.Windows.Shapes;

namespace ZhongTool
{
    /// <summary>
    /// FrmURLEncryptDecrypt.xaml 的交互逻辑
    /// </summary>
    public partial class FrmURLEncodeDecode : Window
    {
        public FrmURLEncodeDecode()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_OnClick(object sender, RoutedEventArgs e)
        {
            txtResult.Text = new EncryptHelper().UrlEncode(txtInput.Text.Trim());
        }

        private void BtnDencrypt_OnClick(object sender, RoutedEventArgs e)
        {
            txtResult.Text = new EncryptHelper().UrlDecode(txtInput.Text.Trim());
        }
    }
}
