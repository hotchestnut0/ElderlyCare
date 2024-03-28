using ElderlyCareApp.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ElderlyCareApp
{
    public class AppLinkConfig : ISavable
    {
        public string? AppName { get; set; }
        public ImageSource? Icon { get; set; }
        public string? Executable { get; set; }

        public void Save(Stream stream)
        {
            if (AppName == null || Icon == null || Executable == null)
                throw new InvalidOperationException("This config is not initialized.");
            using BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, true);
            bw.Write(AppName);
            bw.Write(Executable);

            byte[]? icon = IconUtil.ImageSourceToBytes(Icon);
            if(icon == null)
            {
                throw new ArgumentException("The icon is not a valid bitmap.");
            }
            bw.Write(icon.Length);
            bw.Write(icon);
        }

        public async Task SaveAsync(Stream stream, CancellationToken token = default)
        {
            if (AppName == null || Icon == null || Executable == null)
                throw new InvalidOperationException("This config is not initialized.");

            await using BinaryWriter bw = new BinaryWriter(stream, Encoding.UTF8, true);

            await Task.Run(() =>
            {
                bw.Write(AppName);
                bw.Write(Executable);

                byte[]? icon = IconUtil.ImageSourceToBytes(Icon);
                if (icon == null)
                {
                    throw new ArgumentException("The icon is not a valid bitmap.");
                }
                bw.Write(icon.Length);
                bw.Write(icon);
            }, token);
        }

        public void Read(Stream stream)
        {
            using BinaryReader br = new BinaryReader(stream, Encoding.UTF8, true);
            AppName = br.ReadString();
            Executable = br.ReadString();

            int count = br.ReadInt32();
            byte[] iconBuffer = br.ReadBytes(count);

            Icon = IconUtil.BytesToImageSource(iconBuffer);
        }

        public async Task ReadAsync(Stream stream, CancellationToken token = default)
        {
            using BinaryReader br = new(stream, Encoding.UTF8, true);

            await Task.Run(() =>
            {
                AppName = br.ReadString();
                Executable = br.ReadString();

                int count = br.ReadInt32();
                byte[] iconBuffer = br.ReadBytes(count);

                Icon = IconUtil.BytesToImageSource(iconBuffer);
            }, token);
        }

        public void New()
        {
            
        }
    }
}
