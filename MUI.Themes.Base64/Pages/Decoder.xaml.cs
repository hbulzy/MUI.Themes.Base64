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
            string Base64StringToConvert = string.Empty;
            if (BrowsedFile.Text != "Please choose a file")
            {
                StreamReader streamReader = new StreamReader(BrowsedFile.Text);
                Base64StringToConvert = streamReader.ReadToEnd();
                streamReader.Close();
            }
            else if (Base64String.Text != string.Empty) { Base64StringToConvert = Base64String.Text; }
            else { }
            if (Base64StringToConvert.Contains("data:image/png;base64,")) 
            { 
                Save.Filter = "Portable Network Graphics|*.png";
                Base64StringToConvert.Replace("data:image/png;base64,", string.Empty);
            }
            else if (Base64StringToConvert.Contains("data:image/gif;base64,")) 
            { 
                Save.Filter = "Graphics Interchange Format|*.gif";
                Base64StringToConvert.Replace("data:image/gif;base64,", string.Empty);
            }
            else if (Base64StringToConvert.Contains("data:image/jpg;base64,") | Base64StringToConvert.Contains("data:image/jpeg;base64")) 
            { 
                Save.Filter = "Joint Photographic Experts Group|*.jpg;*.jpeg";
                Base64StringToConvert.Replace("data:image/jpg;base64,", string.Empty);
                Base64StringToConvert.Replace("data:image/jpeg;base64,", string.Empty);
            }
            else if (Base64StringToConvert.Contains("data:image/bmp;base64,"))
            {
                Save.Filter = "Bitmap|*.bmp";
                Base64StringToConvert.Replace("data:image/bmp;base64,", string.Empty);
            }
            else { ModernDialog.ShowMessage("Only supports BMP, JPG, JPEG, GIF and PNG for now.", "Warning", MessageBoxButton.OK); return; }
            Save.CheckPathExists = true;
            Save.ValidateNames = true;
            Nullable<bool> FileSelected = Save.ShowDialog();
            if (FileSelected == true)
            {
                string SaveFile = Save.FileName;
                SavedFile.Text = "   "+SaveFile;
                ResultText.BBCode = "Decoding...";
                try
                {
                    if (Save.Filter == "Portable Network Graphics|*.png")
                    {
                        System.Drawing.Image Result = BitmapToBase64.Base64.ImageFromBase64String(Base64StringToConvert);
                        Result.Save(SaveFile, ImageFormat.Png);
                    }
                    else if (Save.Filter == "Graphics Interchange Format|*.gif")
                    {
                        System.Drawing.Image Result = BitmapToBase64.Base64.ImageFromBase64String(Base64StringToConvert);
                        Result.Save(SaveFile, ImageFormat.Gif);
                    }
                    else if (Save.Filter == "Joint Photographic Experts Group|*.jpg;*.jpeg")
                    {
                        System.Drawing.Image Result = BitmapToBase64.Base64.ImageFromBase64String(Base64StringToConvert);
                        Result.Save(SaveFile, ImageFormat.Jpeg);
                    }
                    else if (Save.Filter == "Bitmap|*.bmp")
                    {
                        
                        System.Drawing.Image Result = BitmapToBase64.Base64.ImageFromBase64String(Base64StringToConvert);
                        Result.Save(SaveFile, ImageFormat.Bmp);
                    }
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