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
    /// FrmFormatVariable.xaml 的交互逻辑
    /// </summary>
    public partial class FrmFormatVariable : Window
    {
        public FrmFormatVariable()
        {
            InitializeComponent();
        }

        private void BtnFormat_Click(object sender, RoutedEventArgs e)
        {
            string input = txtInput.Text.Trim();
            txtOutput.Text = GetFormatText(input);
        }

        private string GetFormatText(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }
            //去除点
            input = input.Replace(".", "");
            string[] arrText = input.Split(new[] { ' ','_'}, StringSplitOptions.RemoveEmptyEntries);
            if (arrText.Length <= 0)
            {
                return input;
            }
            StringBuilder sb = new StringBuilder();
            foreach (var text in arrText)
            {
                sb.Append(UpperFirstCharacter(text));
            }

            return sb.ToString();
        }

        private string UpperFirstCharacter(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return s;
            }

            return s[0].ToString().ToUpper() + s.Substring(1, s.Length - 1).ToLower();
        }
    }
}
