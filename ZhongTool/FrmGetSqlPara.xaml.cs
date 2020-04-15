using System;
using System.Collections.Generic;
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

namespace ZhongTool
{
    /// <summary>
    /// FrmGetSqlPara.xaml 的交互逻辑
    /// </summary>
    public partial class FrmGetSqlPara : Window
    {
        public FrmGetSqlPara()
        {
            InitializeComponent();
        }

        private void btnExtract_Click(object sender, RoutedEventArgs e)
        {
            string paraWithValStr = txtParaWithValue.Text.Trim();
            string paraDefinedStr = txtParaDefined.Text.Trim();
            Regex reg = new Regex(@"([@\w\s]*)=(.+?,)?");
            Dictionary<string, string> dic = new Dictionary<string, string>();
            MatchCollection coll = reg.Matches(paraWithValStr);
            if (coll.Count > 0)
            {
                foreach (Match match in coll)
                {
                    dic[match.Groups[1].Value.Trim()] = match.Groups[2].Value;
                }
            }

            Dictionary<string, string> dic2 = new Dictionary<string, string>();
            Regex reg2 = new Regex(@"(@.+?)\s+(\S+)\s*(,|output)?", RegexOptions.IgnoreCase);
            coll = reg2.Matches(paraDefinedStr);
            if (coll.Count > 0)
            {
                foreach (Match match in coll)
                {
                    dic2[match.Groups[1].Value.Trim()] = match.Groups[2].Value;
                }
            }

            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<string, string> kv in dic)
            {
                string sqlType = string.Empty;
                if (dic2.ContainsKey(kv.Key))
                {
                    sqlType = dic2[kv.Key];
                }

                if (string.IsNullOrWhiteSpace(kv.Value))
                {
                    sb.AppendLine($"{kv.Key} {sqlType}");
                }
                else
                {
                    sb.AppendLine($"{kv.Key} {sqlType} = {kv.Value}");
                }
            }

            txtResult.Text = sb.ToString();
        }
    }
}
