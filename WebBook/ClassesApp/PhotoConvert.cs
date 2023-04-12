using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace WebBook.ClassesApp
{
     class PhotoConvert
    {
        public static BitmapImage loagPhoto(byte[] imagebate)
        {
            if (imagebate == null || imagebate.Length == 0) return null;
            var image = new BitmapImage();
            using (var memoryStream = new MemoryStream(imagebate))
            {
                memoryStream.Position = 0;
                image.BeginInit();
                image.CreateOptions = BitmapCreateOptions.PreservePixelFormat;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.UriSource = null;
                image.StreamSource = memoryStream;
                image.EndInit();
            }
            image.Freeze();
            return image;
        }
    }
}
