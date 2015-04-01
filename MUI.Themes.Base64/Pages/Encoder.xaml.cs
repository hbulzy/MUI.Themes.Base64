using BitmapToBase64;
using FirstFloor.ModernUI.Windows.Controls;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
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
            Browse.Filter = "All Files (*.*)|*.*";
            Browse.CheckFileExists = true;
            Browse.CheckPathExists = true;
            Browse.ValidateNames = true;
            Browse.Multiselect = false;
            Nullable<bool> FileSelected = Browse.ShowDialog();
            if (FileSelected == true)
            {
                string OpenFile = Browse.FileName;
                OpenedFile.Text = "   " + OpenFile;
                FinalResult.Text = "Encoding...";
                string FileType= Path.GetExtension(OpenFile);
                string MimeType= MimeMapping.GetMimeMapping(FileType);
                if (FileType == ".png" | FileType == ".jpg" | FileType == ".jpeg" | FileType == ".gif" | FileType == ".bmp" | FileType == ".ico") { BrowsedImage.Source = new BitmapImage(new Uri(OpenFile, UriKind.Absolute)); }
                if (FileType == ".png") { FinalResult.Text = "data:"+MimeType+";base64," + BitmapToBase64.Base64.ImageToBase64String(System.Drawing.Image.FromFile(OpenFile), ImageFormat.Png); }
                else if (FileType == ".jpg" | FileType == ".jpeg") { FinalResult.Text = "data:"+MimeType+";base64," + BitmapToBase64.Base64.ImageToBase64String(System.Drawing.Image.FromFile(OpenFile), ImageFormat.Jpeg); }
                else if (FileType == ".gif") { FinalResult.Text = "data:"+MimeType+";base64," + BitmapToBase64.Base64.ImageToBase64String(System.Drawing.Image.FromFile(OpenFile), ImageFormat.Gif); }
                else if (FileType == ".bmp") { FinalResult.Text = "data:"+MimeType+";base64," + BitmapToBase64.Base64.ImageToBase64String(System.Drawing.Image.FromFile(OpenFile), ImageFormat.Bmp); }
                else { FinalResult.Text = "data:" + MimeType + ";base64," + BitmapToBase64.Base64.FileToBase64String(OpenFile); }
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
