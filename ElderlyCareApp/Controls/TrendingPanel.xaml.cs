using ElderlyCareApp.NewsSource;
using ElderlyCareApp.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ElderlyCareApp.Controls
{
    /// <summary>
    /// TrendingPanel.xaml 的交互逻辑
    /// </summary>
    public partial class TrendingPanel : UserControl
    {
        private BaiduTrendingProvider? _provider;
        private TrendingPanelViewModel _viewModel;
        public TrendingPanel()
        {
            InitializeComponent();
            _viewModel = (TrendingPanelViewModel)DataContext;
        }

        private async void Trend_Loaded(object sender, RoutedEventArgs e)
        {
            _provider = new();
            bool r = await _provider.FetchAsync();
            if (!r)
            {
                TextBlock textBlock = new TextBlock();
                textBlock.Text = "无法获取资讯，可能没有互联网连接";
                textBlock.Margin = new(5);

                Trends.Children.Add(textBlock);
                return;
            }

            PlaceControls(_provider.GetTrendings());

        }

        private async void Refresh_Click(object sender, RoutedEventArgs e)
        {
            if (_provider == null)
                return;

            _viewModel.EnableRefreshButton = false;
            _viewModel.ShowRefreshComplete = Visibility.Visible;
            bool res = await _provider.FetchAsync();
            if (!res)
                goto reset;

            PlaceControls(_provider.GetTrendings());

            await Task.Delay(TimeSpan.FromSeconds(5));

        reset:
            _viewModel.EnableRefreshButton = true;
            _viewModel.ShowRefreshComplete = Visibility.Collapsed;
        }

        private void PlaceControls(Trending[] trendings)
        {
            Trends.Children.Clear();
            foreach (var i in trendings)
            {
                TrendNewsControl trendNewsControl = new(i);
                trendNewsControl.Margin = new(5);

                Trends.Children.Add(trendNewsControl);
            }
        }
    }
}
