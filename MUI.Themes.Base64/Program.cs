using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;


namespace MUI.Themes.Base64
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.CommandLine.Contains(":\\")) {
            SaveFileDialog Save = new SaveFileDialog();
            Save.Title = "Save as...";
            Save.InitialDirectory = "Desktop";
            string[] OpenFile = Environment.GetCommandLineArgs();
            StreamReader streamReader = new StreamReader(OpenFile[1]);
            string Base64StringToConvertBefore = streamReader.ReadToEnd();
            string Base64StringToConvert = string.Empty;
            streamReader.Close();
            if (Base64StringToConvertBefore.Contains("data:image/png;base64,"))
            {
                Save.Filter = "Portable Network Graphics|*.png";
                Base64StringToConvert = Base64StringToConvertBefore.Replace("data:image/png;base64,", string.Empty);
            }
            else if (Base64StringToConvertBefore.Contains("data:image/gif;base64,"))
            {
                Save.Filter = "Graphics Interchange Format|*.gif";
                Base64StringToConvert = Base64StringToConvertBefore.Replace("data:image/gif;base64,", string.Empty);
            }
            else if (Base64StringToConvertBefore.Contains("data:image/jpg;base64,"))
            {
                Save.Filter = "Joint Photographic Experts Group|*.jpg;*.jpeg";
                Base64StringToConvert = Base64StringToConvertBefore.Replace("data:image/jpg;base64,", string.Empty);
                Base64StringToConvertBefore.Replace("data:image/jpeg;base64,", string.Empty);
            }
            else if (Base64StringToConvertBefore.Contains("data:image/jpeg;base64"))
            {
                Save.Filter = "Joint Photographic Experts Group|*.jpg;*.jpeg";
                Base64StringToConvert = Base64StringToConvertBefore.Replace("data:image/jepg;base64,", string.Empty);
            }
            else { ModernDialog.ShowMessage("Only supports JPG, JPEG, GIF and PNG for now.", "Warning", MessageBoxButton.OK); return; }
            Save.CheckPathExists = true;
            Save.ValidateNames = true;
            Save.ShowDialog();
            if (Save.FileName != string.Empty)
            {
                string SaveFile = Save.FileName;
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

                    System.Windows.Forms.MessageBox.Show("It has been saved as " + SaveFile + ".", "Success!!!", MessageBoxButtons.OK);
                }
                catch (Exception exc) { System.Windows.Forms.MessageBox.Show("The encoding has failed. Don't mess with my files!!!"+exc.ToString(), "Failed!!!", MessageBoxButtons.OK); }
            }
            } 
            else App.Main();
        }
    }
}
