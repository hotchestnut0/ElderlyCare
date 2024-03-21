using ElderlyCareApp.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ElderlyCareApp.Viewmodels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private double _fontSizeRaw;
        private bool _enableDarkMode;
        private bool _autoDarkMode;

        public SettingsProperties SettingsProperties { get; set; } = SettingsProperties.Read();
        public event PropertyChangedEventHandler? PropertyChanged;

        public SettingsViewModel()
        {
            _fontSizeRaw = SettingsProperties.FontSize;
            _enableDarkMode = SettingsProperties.DarkMode == DarkMode.Enabled;
            _autoDarkMode = SettingsProperties.DarkMode == DarkMode.Auto;
        }

        public double FontSizeRaw
        {
            get => _fontSizeRaw;
            set
            {
                SettingsProperties.FontSize = value;
                SetField(ref _fontSizeRaw, value);
                
            } 
        }

        public bool EnableDarkMode
        {
            get => _enableDarkMode;
            set
            {
                SettingsProperties.DarkMode = value ? DarkMode.Enabled : DarkMode.Disabled;
                SetField(ref _enableDarkMode, value);
            }
        }

        public bool AutoDarkMode
        {
            get => _autoDarkMode;
            set
            {
                SettingsProperties.DarkMode = value ? DarkMode.Auto : DarkMode.Enabled;
                SetField(ref _autoDarkMode, value);
            } 
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }


    }
}
