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
    public class TrendingPanelViewModel : INotifyPropertyChanged
    {
        private bool _enableRefreshButton = true;
        private Visibility _showRefreshComplete = Visibility.Collapsed;
        public event PropertyChangedEventHandler? PropertyChanged;

        public bool EnableRefreshButton
        {
            get => _enableRefreshButton;
            set => SetField(ref _enableRefreshButton, value);
        }

        public Visibility ShowRefreshComplete
        {
            get => _showRefreshComplete;
            set => SetField(ref _showRefreshComplete, value);
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
