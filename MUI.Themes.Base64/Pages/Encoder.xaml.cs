using Base64Bitmaps;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;

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
            OpenedFile.Text = "   Please choose a file";
            FinalResult.Text = "The encoding will automatically start when you select a file.";
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
                string OpenFile = Browse.FileName;
                OpenedFile.Text = "   " + OpenFile;
                StreamReader streamReader = new StreamReader(OpenFile);
                Loading.IsActive = true;
                FinalResult.Text = "Encoding...";
                Bitmap bmp = new Bitmap(streamReader.BaseStream);
                streamReader.Close();
                Loading.IsActive = false;
                FinalResult.Text = "data:image/png;base64," + bmp.ToBase64String(ImageFormat.Png);
            }
        }
    }
}
