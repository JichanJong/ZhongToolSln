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
    /// FrmGuid.xaml 的交互逻辑
    /// </summary>
    public partial class FrmGuid : Window
    {
        public FrmGuid()
        {
            InitializeComponent();
        }

        private void BtnGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            var guid = Guid.NewGuid();
            string newLine = Environment.NewLine;
            string result =
                $"{guid:N}{newLine}{guid:D}{newLine}{guid:B}{newLine}{guid:P}";
            txtCode.Text = result;
        }
    }
}
