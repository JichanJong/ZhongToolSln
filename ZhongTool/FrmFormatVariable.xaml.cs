﻿using System;
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
using ZhongTool.Common;

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
            txtOutput.Text = Tools.GetFormatText(input);
        }

        
    }
}
