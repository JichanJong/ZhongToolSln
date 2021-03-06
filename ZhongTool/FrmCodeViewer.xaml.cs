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

namespace ZhongTool
{
    /// <summary>
    /// FrmCodeViewer.xaml 的交互逻辑
    /// </summary>
    public partial class FrmCodeViewer : Window
    {
        public string Code { get; set; }

        public FrmCodeViewer()
        {
            InitializeComponent();
        }

        private void FrmCodeViewer_Loaded(object sender, RoutedEventArgs e)
        {
            txtCode.Text = Code;
        }
    }
}
