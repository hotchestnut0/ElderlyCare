using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;

namespace ElderlyCareApp.Properties
{
    [Serializable]
    public class SettingsProperties
    {
        public delegate void SettingsUpdateEventHandler(object sender);
        public event SettingsUpdateEventHandler? SettingsUpdate;

        public SettingsProperties()
        {
            FontSize = 20d;
            DarkMode = DarkMode.Disabled;
        } 

        public static SettingsProperties Read()
        {
            using FileStream fileStream = File.Open("settings.json", FileMode.OpenOrCreate, FileAccess.Read);
            if (fileStream.Length == 0)
            {
                return new();
            }

            return JsonSerializer.Deserialize<SettingsProperties>(fileStream) ?? new();
        }

        public void Save()
        {
            {
                using FileStream fileStream = File.Create("settings.json");
                JsonSerializer.Serialize(fileStream, this);
            }

            SettingsUpdate?.Invoke(this);
        }

        public double FontSize{ get; set; }
        public DarkMode DarkMode { get; set; }

    }

    public enum DarkMode
    {
        Disabled,
        Enabled,
        Auto
    }
}
