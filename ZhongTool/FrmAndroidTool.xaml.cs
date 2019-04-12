using System;
using System.Collections.Generic;
using System.IO;
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
using System.Xml.Linq;
using Microsoft.Win32;

namespace ZhongTool
{
    /// <summary>
    /// FrmAndroidTool.xaml 的交互逻辑
    /// </summary>
    public partial class FrmAndroidTool : Window
    {
        public FrmAndroidTool()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "xml文件|*.xml";
            if (ofd.ShowDialog() == true)
            {
                txtPath.Text = ofd.FileName;
            }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            string path = txtPath.Text.Trim();
            if (!File.Exists(path))
            {
                MessageBox.Show("路径不存在");
                return;
            }
            XDocument doc = XDocument.Load(path);
            
        }
    }
}
