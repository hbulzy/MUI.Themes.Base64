using Base64Bitmaps;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

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
            SavedFile.Text = "   Please choose a file";
            FinalResult.Text = "The encoding will automatically start when you select a file.";
            BrowsedImage.Source = new BitmapImage(new Uri("Assets/transparent.png", UriKind.Relative));
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
                FinalResult.Text = "Encoding...";
                BrowsedImage.Source = new BitmapImage(new Uri(OpenFile, UriKind.Absolute));
                Bitmap bmp = new Bitmap(streamReader.BaseStream);
                streamReader.Close();
                FinalResult.Text = "data:image/png;base64," + bmp.ToBase64String(ImageFormat.Png);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (FinalResult.Text == "The encoding will automatically start when you select a file.")
            {
                ModernDialog.ShowMessage("You haven't encoded anything, duh.", "Dumb!!!", MessageBoxButton.OK);
            }
            else
                {
            SaveFileDialog Save = new SaveFileDialog();
            Save.Title = "Save as...";
            Save.InitialDirectory = "Desktop";
            Save.Filter = "Text|*.txt";
            Save.CheckPathExists = true;
            Save.ValidateNames = true;
            Nullable<bool> FileSelected = Save.ShowDialog();
            if (FileSelected == true)
            {
                    System.IO.File.WriteAllText(@Save.FileName, FinalResult.Text);
                    string SaveFile = Save.FileName;
                    SavedFile.Text = "   " + SaveFile;
            }
                }
        }
    }
}
