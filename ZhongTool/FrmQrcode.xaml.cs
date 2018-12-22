using System;
using System.Collections.Generic;
using System.Drawing;
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
using Microsoft.Win32;
using QRCoder;
using ZhongTool.Common;

namespace ZhongTool
{
    /// <summary>
    /// FrmQrcode.xaml 的交互逻辑
    /// </summary>
    public partial class FrmQrcode : Window
    {
        private Bitmap qrCodeImage;
        public FrmQrcode()
        {
            InitializeComponent();
        }

        private void BtnGenerate_OnClick(object sender, RoutedEventArgs e)
        {
            string txt = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(txt))
            {
                MessageBox.Show("请先输入内容进行生成");
                return;
            }
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(txt, (QRCodeGenerator.ECCLevel)cmbLevel.SelectedValue);
            QRCode qrCode = new QRCode(qrCodeData);
            qrCodeImage = qrCode.GetGraphic(20);
            BitmapSource source = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(qrCodeImage.GetHbitmap(),IntPtr.Zero,Int32Rect.Empty,BitmapSizeOptions.FromEmptyOptions());
            imgQrCode.Source = source;
        }

        private void BtnSave_OnClick(object sender, RoutedEventArgs e)
        {
            if (qrCodeImage == null)
            {
                MessageBox.Show("请先生成二维码");
                return;
            }
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "图片文件|*.png;*.jpg";
            if (sfd.ShowDialog() == true)
            {
                qrCodeImage.Save(sfd.FileName);
            }
        }

        private void FrmQrcode_OnLoaded(object sender, RoutedEventArgs e)
        {
            List<BindEnumItem> lstData = EnumHelper.EnumToList<QRCodeGenerator.ECCLevel>();
            cmbLevel.ItemsSource = lstData;
            cmbLevel.SelectedIndex = 0;
        }
    }
}
