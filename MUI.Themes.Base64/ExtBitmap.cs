using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Imaging;

namespace Base64Bitmaps
{
    public static class ExtBitmap
    {
        public static string ToBase64String(this Bitmap bmp, ImageFormat imageFormat)
        {
            string base64String = string.Empty;

            MemoryStream memoryStream = new MemoryStream();
            bmp.Save(memoryStream, imageFormat);

            memoryStream.Position = 0;
            byte[] byteBuffer = memoryStream.ToArray();

            memoryStream.Close();

            base64String = Convert.ToBase64String(byteBuffer);
            byteBuffer = null;

            return base64String;
        }

        public static string ToBase64API(this Bitmap bmp, ImageFormat imageFormat)
        {
            string imgAPI = string.Empty;
            string base64String = string.Empty;

            base64String = bmp.ToBase64String(imageFormat);

            imgAPI = "data:image/" + imageFormat.ToString() + ";base64,";
            imgAPI += base64String;

            return imgAPI;
        }

        public static Bitmap Base64StringToBitmap(this string base64String)
        {
            Bitmap bmpReturn = null;

            byte[] byteBuffer = Convert.FromBase64String(base64String);
            MemoryStream memoryStream = new MemoryStream(byteBuffer);

            memoryStream.Position = 0;

            bmpReturn = (Bitmap)Bitmap.FromStream(memoryStream);

            memoryStream.Close();
            memoryStream = null;
            byteBuffer = null;

            return bmpReturn;
        }
    }
}
