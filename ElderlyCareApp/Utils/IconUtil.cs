﻿using Svg;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ElderlyCareApp.Utils
{
    internal class IconUtil
    {
        public static ImageSource? ExtractFileIcon(string path)
        {
            try
            {
                var icon = Icon.ExtractAssociatedIcon(path);
                if (icon == null)
                    return null;
                BitmapSource source =
                    Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                return source;
            }
            catch { return null; }
        }

        public static ImageSource? DownloadSvgImage(string url)
        {
            using HttpClient httpClient = new();
            var stream = httpClient.GetStreamAsync(url).Result;
            SvgDocument svgDocument = SvgDocument.Open<SvgDocument>(stream: stream);

            System.Drawing.Bitmap bitmap = svgDocument.Draw();

            // 将SvgDocument渲染到Bitmap
            return Imaging.CreateBitmapSourceFromHBitmap(bitmap.GetHbitmap(), IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        public static ImageSource DownloadBitmapImage(string url)
        {
            using HttpClient httpClient = new();
            var stream = httpClient.GetStreamAsync(url).Result;

            BitmapImage bitmapImage = new();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            //bitmapImage.Freeze();

            return bitmapImage;

        }
        
        public static async Task<ImageSource> DownloadBitmapImageAsync(string url)
        {
            using HttpClient httpClient = new();
            var stream = await httpClient.GetStreamAsync(url);

            BitmapImage bitmapImage = new();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = stream;
            bitmapImage.EndInit();
            //bitmapImage.Freeze();

            return bitmapImage;

        }

        public static byte[]? ImageSourceToBytes(ImageSource imageSource)
        {
            byte[]? bytes = null;
            try
            {
                PngBitmapEncoder encoder = new();
                if (imageSource is BitmapSource bitmapSource)
                {
                    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));

                    using var stream = new MemoryStream();
                    encoder.Save(stream);
                    bytes = stream.ToArray();
                }

            }
            catch { return null; }

            return bytes;
        }

        public static ImageSource BytesToImageSource(byte[] array)
        {
            using MemoryStream memoryStream = new MemoryStream(array);

            PngBitmapDecoder decoder=new(memoryStream, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.OnLoad);
            return decoder.Frames[0];
        }
    }
}
