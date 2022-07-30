

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;

namespace QulixPhotoStock.Service.Helpers
{
    public static class PictureInfo
    {
#pragma warning disable
        public static  (long sizeOfPicture, string nameOfPictire) GetPictureMemory(string link)
        {
            string path = @$"..\..\..\..\QulixPhotoStock.Data\Contexts\DownloadedPhotos\{Guid.NewGuid()}.png";

            using (WebClient client = new WebClient())
            {
                client.DownloadFile(new Uri(link), path);
            }

            FileInfo fileInfo = new FileInfo(path);
            return (fileInfo.Length, fileInfo.Name);

        }
    }
}
