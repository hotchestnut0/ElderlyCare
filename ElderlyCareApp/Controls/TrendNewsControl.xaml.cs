using ElderlyCareApp.NewsSource;
using ElderlyCareApp.Utils;
using ElderlyCareApp.Viewmodels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
    /// TrendNewsControl.xaml 的交互逻辑
    /// </summary>
    public partial class TrendNewsControl : UserControl
    {
        private TrendNewsControlViewModel _viewModel;
        private Trending _trending;
        public TrendNewsControl()
        {
            InitializeComponent();
            _viewModel = (TrendNewsControlViewModel)DataContext;
        }

        public TrendNewsControl(Trending trending)
        {
            InitializeComponent();
            _viewModel = (TrendNewsControlViewModel)DataContext;

            _trending = trending;
            _viewModel.UpdateTrendText(trending);
        }

        public async void UpdateTrend(Trending trending)
        {
            _viewModel.UpdateTrendText(trending);
            await _viewModel.UpdateTrendAsync(trending);
        }

        private void News_Click(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton != MouseButton.Left)
                return;

            ProcessUtil.StartProcessShellExecute(_viewModel.TrendLink);
           
        }

        private async void NewsControl_Loaded(object sender, RoutedEventArgs e)
        {
            await _viewModel.UpdateTrendImageAsync(_trending);
        }
    }
}
