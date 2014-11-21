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

            Save.Title = "Save as...";
            Save.InitialDirectory = "Desktop";
            Save.Filter = "Portable Network Graphics|*.png";
            Save.CheckPathExists = true;
            Save.ValidateNames = true;
            Nullable<bool> FileSelected = Save.ShowDialog();
            if (FileSelected == true)
            {
                string File = Save.FileName;
                OpenedFile.Text = "   "+File;
            }
        }
    }
}