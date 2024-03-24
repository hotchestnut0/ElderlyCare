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
using ElderlyCareApp.Utils;

namespace ElderlyCareApp.Controls
{
    /// <summary>
    /// WeatherDisplay.xaml 的交互逻辑
    /// </summary>
    public partial class WeatherDisplay : UserControl
    {
        private WeatherHelper _weatherHelper;
        public WeatherDisplay()
        {
            InitializeComponent();
            _weatherHelper = new();
            
        }

        private async void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            await _weatherHelper.FetchAsync();
        }

        private void Weather_Show_Detail(object sender, RoutedEventArgs e)
        {
            _weatherHelper.GotoDetail();
        }
    }
}
