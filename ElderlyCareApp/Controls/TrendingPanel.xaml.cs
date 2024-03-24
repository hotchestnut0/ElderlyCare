using ElderlyCareApp.Utils;
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
        private NewsHelper _newsHelper;
        public TrendingPanel()
        {
            InitializeComponent();
        }

        private async void Trend_Loaded(object sender, RoutedEventArgs e)
        {
            _newsHelper = new();
            await _newsHelper.FetchAsync();

            foreach(var i in _newsHelper.GetTrends())
            {
                TrendNewsControl trendNewsControl = new(i);
                trendNewsControl.Margin = new(5);

                Trends.Children.Add(trendNewsControl);
            }
        }
    }
}
