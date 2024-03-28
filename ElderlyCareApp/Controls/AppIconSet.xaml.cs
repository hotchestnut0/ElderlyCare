using ElderlyCareApp.Viewmodels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// AppIconSet.xaml 的交互逻辑
    /// </summary>
    public partial class AppIconSet : UserControl
    {
        private AppIconSetViewModel _viewModel;
        public AppIconSet()
        {
            InitializeComponent();
            _viewModel = (AppIconSetViewModel)DataContext;
            
        }

        private void MenuItem_AddApp(object sender, RoutedEventArgs e)
        {
            AppSelectWindow appSelectWindow = new();
            var result = appSelectWindow.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;

            AppIcon appIcon = new(appSelectWindow.AppFullPath!, appSelectWindow.AppIcon, appSelectWindow.LabelText);
            appIcon.Visibility = Visibility.Visible;
            appIcon.Margin = new(10);
            appIcon.Width = 72;
            appIcon.Height = 72;
            Apps.Children.Add(appIcon);
        }

        public void SaveIcon()
        {

        }
    }
}
