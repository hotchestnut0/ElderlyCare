using ElderlyCareApp.Utils;
using ElderlyCareApp.Viewmodels;
using HandyControl.Controls;
using HandyControl.Data;
using HandyControl.Themes;
using HandyControl.Tools;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ElderlyCareApp.Controls
{
    /// <summary>
    /// AppSelectWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AppSelectWindow : HandyControl.Controls.Window
    {
        private AppSelectWindowViewModel _viewModel;
        public string? AppFullPath => _viewModel.AppFullPath;

        public ImageSource? AppIcon => _viewModel.AppIcon;

        public string? LabelText => _viewModel.LabelText;

        private ImageSource? _defaultIcon;

        public AppSelectWindow()
        {
            InitializeComponent();
            _viewModel = (AppSelectWindowViewModel)DataContext;
            UpdateSkin(MainWindow.CurrentSkinType);
        }

        private void Button_Ok_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(AppFullPath))
            {
                Growl.Warning("请输入文件或URL。");
                return;
            }
            if (!File.Exists(AppFullPath) && !Uri.TryCreate(AppFullPath, UriKind.RelativeOrAbsolute, out _))
            {
                Growl.Warning("选择的文件不存在。");
                return;
            }
            DialogResult = true;
            Close();
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
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

        private void Button_SelectFile_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new();
            var re = openFileDialog.ShowDialog();
            if (!re.HasValue || !re.Value)
            {
                return;
            }

            _viewModel.AppFullPath = openFileDialog.FileName;
            if (File.Exists(AppFullPath))
                _viewModel.AppIcon = _defaultIcon = IconUtil.ExtractFileIcon(AppFullPath);
        }

        private void Image_SetCustomIcon(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton != MouseButtonState.Released)
                return;

            OpenFileDialog openFileDialog = new();
            openFileDialog.Filter = "位图文件|*.png;*.jpg;*.jpeg;*.bmp;*.gif;*.tif|全部文件|*.*";
            openFileDialog.Title = "选择图像";
            bool? result = openFileDialog.ShowDialog();
            if (!result.HasValue || !result.Value)
                return;

            try
            {
                BitmapImage bitmapImage = new(new Uri(openFileDialog.FileName));
                _viewModel.AppIcon = bitmapImage;
            }
            catch(Exception ex)
            {
                Growl.Error($"无法打开图像：{ex.Message}");
                return;
            }
        }

        private void Button_Icon_Restore_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.AppIcon = _defaultIcon;
        }
    }
}
