using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            if (operateType == RegexOperateType.Match)
            {
                MatchString();
            }
            else
            {
                ReplaceString();
            }
        }

        private void ReplaceString()
        {
            try
            {
                Regex regex = GetRegex();
                txtOutput.Text = regex.Replace(txtInput.Text,txtReplaceText.Text);
            }
            catch (Exception e)
            {
                MessageBox.Show("发生错误：" + e);
            }
        }

        private void MatchString()
        {
            try
            {
                Regex regex = GetRegex();
                MatchCollection matchCollection = regex.Matches(txtInput.Text);
                GridView gridView = new GridView();
                ObservableCollection<object> lstData = new ObservableCollection<object>();
                if (matchCollection.Count > 0)
                {
                    int groups = matchCollection[0].Groups.Count;
                    //header
                    int index = 0;
                    Match m = matchCollection[0];
                    GridViewColumn gridViewColumn = new GridViewColumn();
                    gridViewColumn.Header = "Group " + index.ToString();
                    gridViewColumn.DisplayMemberBinding = new Binding(index.ToString());
                    gridView.Columns.Add(gridViewColumn);
                    index++;
                    for (int i = 1; i < groups; i++)
                    {
                        if (m.Groups[i].Success)
                        {
                            gridViewColumn = new GridViewColumn();
                            gridViewColumn.Header = "Group " + index.ToString();
                            gridViewColumn.DisplayMemberBinding = new Binding(index.ToString());
                            gridView.Columns.Add(gridViewColumn);
                            index++;
                        }
                        
                    }

                    
                    //column
                    
                    foreach (Match mc in matchCollection)
                    {
                        dynamic obj = new DynamicModel();
                        obj.PropertyName = "Group 0";
                        obj.Property = mc.Value;
                        index = 1;
                        for (int i = 1; i < groups; i++)
                        {
                            if (mc.Groups[i].Success)
                            {
                                obj.PropertyName = "Group " + index;
                                obj.Property = mc.Groups[i].Value;
                                index++;
                            }
                        }
                        lstData.Add(obj);
                    }
                }

                DataContext = this;
                viewResult.ItemsSource = lstData;
                viewResult.View = gridView;
            }
            catch (Exception e)
            {
                MessageBox.Show("发生错误：" + e);
            }
        }

        private Regex GetRegex()
        {       
            RegexOptions regexOptions = RegexOptions.None;
            if (chkIgnoreCase.IsChecked == true)
            {
                regexOptions = regexOptions | regexOptions & RegexOptions.IgnoreCase;
            }

            if (chkSingleLine.IsChecked == true)
            {
                regexOptions = regexOptions | RegexOptions.Singleline;
            }

            if (chkMultiLine.IsChecked == true)
            {
                regexOptions = regexOptions | RegexOptions.Multiline;
            }

            if (chkIgnoreCulture.IsChecked == true)
            {
                regexOptions = regexOptions | RegexOptions.CultureInvariant;
            }

            if (chkRightToLeft.IsChecked == true)
            {
                regexOptions = regexOptions | RegexOptions.RightToLeft;
            }

            if (chkIgnoreWhiteSpace.IsChecked == true)
            {
                regexOptions = regexOptions | RegexOptions.IgnorePatternWhitespace;
            }
            Regex regex = new Regex(txtRegex.Text, regexOptions);
            return regex;
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
                txtOutput.Visibility = Visibility.Visible;
                viewResult.Visibility = Visibility.Collapsed;
            }
            else
            {
                rowReplaceText.Height = new GridLength(0);
                txtOutput.Visibility = Visibility.Collapsed;
                viewResult.Visibility = Visibility.Visible;
            }
        }
    }

    public class DynamicModel : DynamicObject
    {
        private string propertyName;
        public string PropertyName
        {
            get { return propertyName; }
            set { propertyName = value; }
        }

        // The inner dictionary.
        Dictionary<string, object> dicProperty
            = new Dictionary<string, object>();
        public Dictionary<string, object> DicProperty
        {
            get
            {
                return dicProperty;
            }
        }


        // This property returns the number of elements
        // in the inner dictionary.
        public int Count
        {
            get
            {
                return dicProperty.Count;
            }
        }

        // If you try to get a value of a property 
        // not defined in the class, this method is called.
        public override bool TryGetMember(
            GetMemberBinder binder, out object result)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            string name = binder.Name;

            // If the property name is found in a dictionary,
            // set the result parameter to the property value and return true.
            // Otherwise, return false.
            return dicProperty.TryGetValue(name, out result);
        }

        // If you try to set a value of a property that is
        // not defined in the class, this method is called.
        public override bool TrySetMember(
            SetMemberBinder binder, object value)
        {
            // Converting the property name to lowercase
            // so that property names become case-insensitive.
            if (binder.Name == "Property")
            {
                dicProperty[PropertyName] = value;
            }
            else
            {
                dicProperty[binder.Name] = value;
            }


            // You can always add a value to a dictionary,
            // so this method always returns true.
            return true;
        }
    }
}
