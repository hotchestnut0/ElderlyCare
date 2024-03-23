using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using ElderlyCareApp.Properties;
using ElderlyCareApp.Viewmodels;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;

namespace ElderlyCareApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : BlurWindow
    {
        private SkinType _currentSkinType;
        public MainWindow()
        {
            InitializeComponent();
            SettingsObject.SettingsProperties.SettingsUpdate += SettingsProperties_SettingsUpdate;
            UpdateSkin(_currentSkinType = GetSkinType(SettingsObject.SettingsProperties.DarkMode));
            SyncSettings();
            EnsureNotCoverTaskbar();
        }

        private void EnsureNotCoverTaskbar()
        {
            Width = SystemParameters.FullPrimaryScreenWidth;
            Height = SystemParameters.FullPrimaryScreenHeight + SystemParameters.WindowCaptionHeight;
        }

        private void SettingsProperties_SettingsUpdate(object sender)
        {
            SettingsProperties settings = (SettingsProperties)sender;
            MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)DataContext;

            SkinType newType = GetSkinType(settings.DarkMode);
            if (_currentSkinType != newType)
            {
                RestartApp();
            }
            
            Dispatcher.Invoke(() =>
            {
                mainWindowViewModel.FontSize = settings.FontSize;
            });
        }

        private void SyncSettings()
        {
            SettingsProperties settings = SettingsObject.SettingsProperties;
            MainWindowViewModel mainWindowViewModel = (MainWindowViewModel)DataContext;
            mainWindowViewModel.FontSize = settings.FontSize;
        }

        private void RestartApp()
        {
            Process.Start(Environment.ProcessPath!);
            Application.Current.Shutdown();
        }

        private SkinType GetSkinType(DarkMode darkSetting)
        {
            SkinType skinType = darkSetting switch
            {
                DarkMode.Enabled => SkinType.Dark,
                DarkMode.Disabled => SkinType.Default,
                DarkMode.Auto => DateTime.Now.Hour is > 18 or < 7 ? SkinType.Dark : SkinType.Default,
                _ => SkinType.Default
            };

            return skinType;
        }

        private void SettingsInstance_ThemeChange(bool darkMode)
        {
            SkinType skinType = darkMode ? SkinType.Dark : SkinType.Default;
            UpdateSkin(skinType);
        }

        private void Button_Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        internal void UpdateSkin(SkinType skin)
        {
            SharedResourceDictionary.SharedDictionaries.Clear();
            Resources.MergedDictionaries.Clear();
            Resources.MergedDictionaries.Add(ResourceHelper.GetSkin(skin));
            Resources.MergedDictionaries.Add(new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/HandyControl;component/Themes/Theme.xaml")
            });
        }
    }
}
