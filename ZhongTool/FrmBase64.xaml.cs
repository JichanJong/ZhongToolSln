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
    /// FrmBase64.xaml 的交互逻辑
    /// </summary>
    public partial class FrmBase64 : Window
    {
        public FrmBase64()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_OnClick(object sender, RoutedEventArgs e)
        {
            txtResult.Text = new EncryptHelper().EncryptToBase64String(txtInput.Text.Trim());
        }

        private void BtnDEncrypt_OnClick(object sender, RoutedEventArgs e)
        {
            txtResult.Text = new EncryptHelper().DecryptFromBase64String(txtInput.Text.Trim());
        }
    }
}
