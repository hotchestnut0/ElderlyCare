using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Color = System.Windows.Media.Color;

namespace ElderlyCareApp.Controls
{
    /// <summary>
    /// AppIcon.xaml 的交互逻辑
    /// </summary>
    public partial class AppIcon : UserControl
    {
        public static readonly DependencyProperty AppIconProperty
            = DependencyProperty.Register(nameof(AppIconSource), typeof(ImageSource), typeof(AppIcon), new(null));

        public static readonly DependencyProperty AppNameProperty
            = DependencyProperty.Register(nameof(AppName), typeof(string), typeof(AppIcon), new("图标"));

        public ImageSource AppIconSource
        {
            get => (ImageSource)GetValue(AppIconProperty);
            set => SetValue(AppIconProperty, value);
        }

        public string AppName
        {
            get => (string)GetValue(AppNameProperty);
            set => SetValue(AppNameProperty, value);
        }

        public string? Executable
        {
            get => _executable;
            set
            {
                SetExecutable(value);
                _executable = value;
            } 
        }

        private SolidColorBrush _mouseEnter = new(Color.FromArgb(255, 220, 220, 220));
        private SolidColorBrush _mouseDown = new(Color.FromArgb(255, 200, 200, 200));
        private SolidColorBrush _normal = new(Color.FromArgb(255, 255, 255, 255));
        private ImageSource _defaultIcon = GetDefaultIcon();
        private string? _executable;


        public AppIcon()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void SetExecutable(string executable)
        {
            AppIconSource = ExtractFileIcon(executable) ?? _defaultIcon;
        }

        #region Visual State
        private void AppIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = _mouseEnter;
        }

        private void AppIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = _normal;
        }

        private void AppIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Background = _mouseDown;
        }

        private void AppIcon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Background = _mouseEnter;
            try
            {
                Process.Start(Executable);
            }
            catch
            {

            }
        }
        #endregion

        private ImageSource? ExtractFileIcon(string path)
        {
            try
            {
                var icon = Icon.ExtractAssociatedIcon(path);
                if (icon == null)
                    return null;
                BitmapSource source =
                    Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());

                return source;
            }
            catch { return null; }
        }

        private static ImageSource GetDefaultIcon()
        {
            var iconStream = Application.GetResourceStream(new Uri("pack://application:,,,/defaultIcon.ico"))!.Stream;
            iconStream.Position = 0;
            Icon icon = new(iconStream);
            BitmapSource source =
                Imaging.CreateBitmapSourceFromHIcon(icon.Handle, Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
            return source;
        }
    }
}
