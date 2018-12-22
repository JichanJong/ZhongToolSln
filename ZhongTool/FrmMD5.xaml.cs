using System.Windows;

namespace ZhongTool
{
    /// <summary>
    /// FrmMD5.xaml 的交互逻辑
    /// </summary>
    public partial class FrmMD5 : Window
    {
        public FrmMD5()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_OnClick(object sender, RoutedEventArgs e)
        {
            string input = txtSource.Text.Trim();
            txtResult.Text = new EncryptHelper().MD5Encrypt(input);
        }
    }
}
