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
using ZhongTool.Common;
using ZhongTool.Model;

namespace ZhongTool
{
    /// <summary>
    /// FrmRegex.xaml 的交互逻辑
    /// </summary>
    public partial class FrmRegex : Window
    {
        public FrmRegex()
        {
            InitializeComponent();
        }

        private void BtnMatch_Click(object sender, RoutedEventArgs e)
        {
            

        }

        private void BtnReplace_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void FrmRegex_Loaded(object sender, RoutedEventArgs e)
        {
            List<BindEnumItem> lstData = EnumHelper.EnumToList<RegexOperateType>();
            cmbOperate.ItemsSource = lstData;
            cmbOperate.SelectedIndex = 0;
        }

        private void BtnTest_Click(object sender, RoutedEventArgs e)
        {
            RegexOperateType operateType = (RegexOperateType)cmbOperate.SelectedValue;

        }

        private void CmbOperate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SwitchControl();
        }

        private void SwitchControl()
        {
            RegexOperateType operateType = (RegexOperateType)cmbOperate.SelectedValue;
            if (operateType == RegexOperateType.Replace)
            {
                rowReplaceText.Height = new GridLength(1,GridUnitType.Star);
            }
            else
            {
                rowReplaceText.Height = new GridLength(0);
            }
        }
    }
}
