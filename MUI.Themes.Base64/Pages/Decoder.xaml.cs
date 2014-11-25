using Base64Bitmaps;
using Microsoft.Win32;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows;
using System.Windows.Controls;

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
                if (Base64String.Text.IndexOf("data:image/png;base64,") == -1)
                    {
                        ResultText.BBCode = "Good try, maybe another time.";
                    }
                else
                    {
                        ResultText.BBCode = "Decoding...";
                        Loading.IsActive = true;
                        string Base64 = Base64String.Text.Replace("data:image/png;base64,", string.Empty);
                        Base64.Base64StringToBitmap().Save(SaveFile, ImageFormat.Png);
                        Loading.IsActive = false;
                        Base64String.Text = "";
                        ResultText.BBCode = "It has been saved as " + SaveFile + ".";
                    }
            }
        }
    }
}