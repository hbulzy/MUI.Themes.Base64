using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;

namespace BitmapToBase64
{
    public static class Base64
    {

        public static string FileToBase64String(string path)
        {
            FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            byte[] buffer = new byte[fileStream.Length];
            fileStream.Read(buffer, 0, (int)fileStream.Length);

            MemoryStream memory = new MemoryStream(buffer);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();

            return base64;
        }
        public static string ImageToBase64String(Image image, ImageFormat format)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, image.RawFormat);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();

            return base64;
        }
        public static string IconToBase64String(Icon image)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();

            return base64;
        }

        // The following methods can be used to reverse this process.
        public static Image ImageFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
            Image result = Image.FromStream(memory);
            memory.Close();

            return result;
        }
        public static Icon IconFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64)); 
            Icon result = new Icon(memory);
            memory.Close();
            
            return result;
        }
        public static Cursor CursorFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
            Cursor result = new Cursor(memory);
            memory.Close();

            return result;
        }
    }
}
