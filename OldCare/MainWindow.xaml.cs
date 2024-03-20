using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;
using OldCare.Properties;
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

namespace OldCare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BlurWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = SettingsProperties.SettingsInstance;
            UpdateSkin(SkinType.Dark);
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        internal void UpdateSkin(SkinType skin)
        {
            Dispatcher.Invoke(() =>
            {
                SharedResourceDictionary.SharedDictionaries.Clear();
                Resources.MergedDictionaries.Add(ResourceHelper.GetSkin(skin));
                Resources.MergedDictionaries.Add(new ResourceDictionary
                {
                    Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
                });
            });
        }
    }
}
