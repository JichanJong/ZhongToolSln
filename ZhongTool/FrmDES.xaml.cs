using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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
    /// FrmDES.xaml 的交互逻辑
    /// </summary>
    public partial class FrmDES : Window
    {

        public FrmDES()
        {
            InitializeComponent();
        }

        private void BtnEncrypt_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                txtOutput.Text = Encrypt(input);
            }
        }

        private string Decrypt(string cryptedString)
        {
            string password = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(password))
            {
                password = "123456";
            }
            else if (password.Length < 8)
            {
                password = password.PadRight(8);
            }
            else
            {
                password = password.Substring(0, 8);
            }
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream
                (Convert.FromBase64String(cryptedString));
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateDecryptor(bytes, bytes), CryptoStreamMode.Read);
            StreamReader reader = new StreamReader(cryptoStream);
            return reader.ReadToEnd();
        }

        private string Encrypt(string originalString)
        {
            string password = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(password))
            {
                password = "12345678";
            }
            else if(password.Length < 8)
            {
                password = password.PadRight(8);
            }
            else
            {
                password = password.Substring(0, 8);
            }
            byte[] bytes = Encoding.ASCII.GetBytes(password);
            DESCryptoServiceProvider cryptoProvider = new DESCryptoServiceProvider();
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream,
                cryptoProvider.CreateEncryptor(bytes, bytes), CryptoStreamMode.Write);
            StreamWriter writer = new StreamWriter(cryptoStream);
            writer.Write(originalString);
            writer.Flush();
            cryptoStream.FlushFinalBlock();
            writer.Flush();
            return Convert.ToBase64String(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
        }

        private void BtnDencrypt_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text.Trim();
            if (!string.IsNullOrEmpty(input))
            {
                txtOutput.Text = Decrypt(input);
            }
        }
    }
}
