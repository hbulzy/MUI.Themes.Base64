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
    /// Interaction logic for Decoder.xaml
    /// </summary>
    public partial class Decoder : UserControl
    {
        public Decoder()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (Base64String.Text == string.Empty && BrowsedFile.Text == "Please choose a file")
            {
                ModernDialog.ShowMessage("You haven't typed something or choosen a .txt file to convert, duh.", "Dumb!!!", MessageBoxButton.OK);
            }
            else
            {
            SaveFileDialog Save = new SaveFileDialog();
            SavedFile.Text = "   Please choose a file";
            ResultText.BBCode = "The decoding will begin automatically after you select a file.";
            Save.Title = "Save as...";
            Save.InitialDirectory = "Desktop";
            Save.Filter = "Portable Network Graphics|*.png";
            Save.CheckPathExists = true;
            Save.ValidateNames = true;
            Nullable<bool> FileSelected = Save.ShowDialog();
            if (FileSelected == true)
            {
                string SaveFile = Save.FileName;
                SavedFile.Text = "   "+SaveFile;
                ResultText.BBCode = "Decoding...";
                string Base64StringToConvert = string.Empty;
                if (BrowsedFile.Text != "Please choose a file")
                {
                    StreamReader streamReader = new StreamReader(BrowsedFile.Text);
                    Base64StringToConvert = streamReader.ReadToEnd();
                    streamReader.Close();
                }
                else if (Base64String.Text != string.Empty) {Base64StringToConvert = Base64String.Text;}
                else{}
                try
                {
                    Base64StringToConvert.Replace("data:image/png;base64,", string.Empty).Base64StringToBitmap().Save(SaveFile, ImageFormat.Png);
                    ResultText.BBCode = "It has also been saved as " + SaveFile + ".";
                }
                catch (Exception){ResultText.BBCode = "Good try, maybe another time.";}
                ResultImage.Source = new BitmapImage(new Uri(SaveFile, UriKind.Absolute));
            }
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ResultText.BBCode = "The decoding will begin automatically after you select a file.";
            Base64String.Text = string.Empty;
            SavedFile.Text = "   Please choose a file";
            ResultImage.Source = new BitmapImage(new Uri("Assets/transparent.png", UriKind.Relative));
            OpenFileDialog Browse = new OpenFileDialog();
            Browse.Title = "Browse...";
            Browse.InitialDirectory = "Desktop";
            Browse.Filter = "Text|*.txt";
            Browse.CheckFileExists = true;
            Browse.CheckPathExists = true;
            Browse.ValidateNames = true;
            Browse.Multiselect = false;
            Nullable<bool> FileSelected = Browse.ShowDialog();
            if (FileSelected == true)
            {
                string OpenFile = Browse.FileName;
                BrowsedFile.Text = OpenFile;
            }
        }

        private void Base64String_KeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            ResultText.BBCode = "The decoding will begin automatically after you select a file.";
            BrowsedFile.Text = "Please choose a file";
            SavedFile.Text = "   Please choose a file";
            ResultImage.Source = new BitmapImage(new Uri("transparent.png", UriKind.Relative));
        }
    }
}