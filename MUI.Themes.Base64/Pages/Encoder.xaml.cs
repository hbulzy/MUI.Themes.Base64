using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Base64Bitmaps;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using FirstFloor.ModernUI.Windows.Controls;

namespace MUI.Themes.Base64.Pages
{
    /// <summary>
    /// Interaction logic for Encoder.xaml
    /// </summary>
    public partial class Encoder : UserControl
    {
        public Encoder()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog Browse = new OpenFileDialog();

            Browse.Title = "Browse...";
            Browse.InitialDirectory = "Desktop";
            Browse.Filter = "Portable Network Graphics|*.png";
            Browse.CheckFileExists = true;
            Browse.CheckPathExists = true;
            Browse.ValidateNames = true;
            Browse.Multiselect = false;
            Nullable<bool> FileSelected = Browse.ShowDialog();
            if (FileSelected == true)
            {
                string File = Browse.FileName;
                OpenedFile.Text = "   "+File;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            StreamReader streamReader = new StreamReader("Temp.png");
            Bitmap bmp = new Bitmap(streamReader.BaseStream);
            streamReader.Close();
            string base64 = bmp.ToBase64API(ImageFormat.Png);
            var result = FinalResult.Text = base64;
        }
    }
}
