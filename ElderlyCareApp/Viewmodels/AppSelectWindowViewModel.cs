using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace ElderlyCareApp.Viewmodels
{
    internal class AppSelectWindowViewModel : INotifyPropertyChanged
    {
        private string? _appFullPath;
        private ImageSource? _appIcon;
        private string? _labelText;
        public event PropertyChangedEventHandler? PropertyChanged;

        public string? AppFullPath
        {
            get => _appFullPath;
            set => SetField(ref _appFullPath, value);
        }

        public ImageSource? AppIcon
        {
            get => _appIcon;
            set => SetField(ref _appIcon, value);
        }

        public string? LabelText
        {
            get => _labelText;
            set => SetField(ref _labelText, value);
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
