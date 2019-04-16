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
using System.Xml;
using System.Xml.Linq;
using System.Xml.XPath;
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

            string conent = File.ReadAllText(path);
            XmlReader reader = XmlReader.Create(new StringReader(conent));
            XElement root = XElement.Load(reader);
            XmlNameTable nameTable = reader.NameTable;
            XmlNamespaceManager namespaceManager = new XmlNamespaceManager(nameTable);
            namespaceManager.AddNamespace("android", "http://schemas.android.com/apk/res/android");
            var elements = root.XPathSelectElements("//*[@android:id]", namespaceManager);
            if (elements.Any())
            {
                StringBuilder sbDefined = new StringBuilder();
                StringBuilder sbFind = new StringBuilder();
                foreach (XElement element in elements)
                {
                    string id = element.Attribute("{http://schemas.android.com/apk/res/android}id")?.Value;
                    if (!string.IsNullOrEmpty(id) && id.StartsWith("@+id/"))
                    {
                        id = id.TrimStart("@+id/".ToCharArray());
                    }
                    sbDefined.AppendLine($"    private {element.Name} m{element.Name}{id};");

                    sbFind.AppendLine($"        m{element.Name}{id} = findViewById(R.id.{id});");
                }

                txtCode.Text = $@"public class ExampleActivity extends AppCompatActivity {{
{sbDefined.ToString()}
    @Override
    protected void onCreate(Bundle savedInstanceState) {{
        super.onCreate(savedInstanceState);
        //...
{sbFind.ToString()}
    }}
}}";
            }
        }
    }
}
