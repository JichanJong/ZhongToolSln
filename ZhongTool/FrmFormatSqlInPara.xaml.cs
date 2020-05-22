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
    /// FrmFormatSqlInPara.xaml 的交互逻辑
    /// </summary>
    public partial class FrmFormatSqlInPara : Window
    {
        public FrmFormatSqlInPara()
        {
            InitializeComponent();
        }

        private void btnFormat_Click(object sender, RoutedEventArgs e)
        {
            string str = txtInput.Text.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                bool isString = chkIsString.IsChecked ?? false;
                string[] arrInput = str.Split(new[] { '\r', '\n' },StringSplitOptions.RemoveEmptyEntries);
                IEnumerable<string> strs = isString ? arrInput.Select(x => "'" + x + "'").ToArray() : arrInput.ToArray();
                txtOutput.Text = string.Join(",", strs);
            }
        }
    }
}
