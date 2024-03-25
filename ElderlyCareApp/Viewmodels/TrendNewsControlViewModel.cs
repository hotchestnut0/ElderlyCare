using ElderlyCareApp.NewsSource;
using ElderlyCareApp.Utils;
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
    internal class TrendNewsControlViewModel : INotifyPropertyChanged
    {
        private ImageSource? _trendImage;
        private string _trendTitle;
        private string _trendBrief;
        private int _trendNo;
        private string _trendLink;
        public event PropertyChangedEventHandler? PropertyChanged;

        public ImageSource? TrendImage
        {
            get => _trendImage;
            set => SetField(ref _trendImage, value);
        }

        public string TrendTitle
        {
            get => _trendTitle;
            set => SetField(ref _trendTitle, value);
        }

        public string TrendBrief
        {
            get => _trendBrief;
            set => SetField(ref _trendBrief, value);
        }

        public int TrendNo
        {
            get => _trendNo;
            set => SetField(ref _trendNo, value);
        }

        public string TrendLink
        {
            get => _trendLink;
            set => SetField(ref _trendLink, value);
        }

        public void UpdateTrendText(Trending trending)
        {
            TrendTitle = trending.Title;
            TrendBrief = trending.Description;
            TrendNo = trending.No;
            TrendLink = trending.Link;
        }

        public async Task UpdateTrendImageAsync(Trending trending)
        {
            TrendImage = await IconUtil.DownloadBitmapImageAsync(trending.ImageUrl);
        }

        public async Task UpdateTrendAsync(Trending trending)
        {
            TrendTitle = trending.Title;
            TrendBrief = trending.Description;
            TrendNo = trending.No;
            TrendImage = await IconUtil.DownloadBitmapImageAsync(trending.ImageUrl);
            TrendLink = trending.Link;
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
