using System.Windows;
using System.Windows.Controls;
using ElderlyCareApp.Properties;
using ElderlyCareApp.Viewmodels;

namespace ElderlyCareApp
{
    /// <summary>
    /// Settings.xaml 的交互逻辑
    /// </summary>
    public partial class Settings : UserControl
    {
        public SettingsProperties SettingsProperties { get; set; }

        
        public Settings()
        {
            InitializeComponent();
            SettingsProperties = ((SettingsViewModel)DataContext).SettingsProperties;
            
        }


        private void Button_Save_Settings_Click(object sender, RoutedEventArgs e)
        {
            SettingsProperties.Save();
        }
    }
}
