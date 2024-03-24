using ElderlyCareApp.Utils;
using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Brush = System.Windows.Media.Brush;
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
            = DependencyProperty.Register(nameof(AppName), typeof(string), typeof(AppIcon), new("应用程序"));

        public static readonly DependencyProperty CustomBorderBrushProperty =
            DependencyProperty.Register(nameof(CustomBorderBrush), typeof(Brush), typeof(AppIcon), new(null));

        public static readonly DependencyProperty BackgroundBrushProperty
            = DependencyProperty.Register(nameof(BackgroundBrush), typeof(Brush), typeof(AppIcon));

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
            set => SetExecutable(value);
        }

        public Brush CustomBorderBrush
        {
            get => (Brush)GetValue(CustomBorderBrushProperty);
            set => SetValue(CustomBorderBrushProperty, value);
        }
        public Brush BackgroundBrush
        {
            get => (Brush)GetValue(BackgroundBrushProperty);
            set => SetValue(BackgroundBrushProperty, value);
        }

        private static Brush _enterBrush = new SolidColorBrush(Color.FromArgb(255, 173, 216, 230));
        private static Brush _transBrush = new SolidColorBrush(Color.FromArgb(0, 255, 255, 255));
        private static Brush _pressBrush = new SolidColorBrush(Color.FromArgb(255, 190, 190, 190));
        private static Brush _defaultBrush;

        private ImageSource _defaultIcon = GetDefaultIcon();
        private string? _executable;
        private bool _useCustomIcon;


        public AppIcon()
        {
            DataContext = this;
            InitializeComponent();

        }

        public AppIcon(string executable, ImageSource? icon, string? label)
        {
            if (!File.Exists(executable) && !Uri.TryCreate(executable, UriKind.RelativeOrAbsolute, out _))
                throw new FileNotFoundException("The executable is not found.");

            DataContext = this;
            InitializeComponent();

            if (icon != null)
            {
                _useCustomIcon = true;
                AppIconSource = icon;
            }
            SetExecutable(executable);
            AppName = label ?? "应用程序";
        }

        private void SetExecutable(string? executable)
        {
            _executable = executable;
            if (string.IsNullOrWhiteSpace(executable))
            {
                AppIconSource = _defaultIcon;
                return;
            }
            if (!_useCustomIcon)
                AppIconSource = IconUtil.ExtractFileIcon(executable) ?? _defaultIcon;
        }

        public void SetCustomIcon(ImageSource imageSource)
        {
            _useCustomIcon = true;
            AppIconSource = imageSource;
        }

        public void UseAutoIcon()
        {
            _useCustomIcon = false;
            SetExecutable(_executable);
        }

        #region Visual State
        private void AppIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            CustomBorderBrush = _enterBrush;
        }

        private void AppIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            CustomBorderBrush = _transBrush;
        }

        private void AppIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            BackgroundBrush = (SolidColorBrush)_pressBrush;
        }

        private void AppIcon_MouseUp(object sender, MouseButtonEventArgs e)
        {
            BackgroundBrush = (SolidColorBrush)_defaultBrush;
            if (e.ChangedButton != MouseButton.Left)
                return;
            try
            {
                Process process = new();
                process.StartInfo = new ProcessStartInfo()
                {
                    FileName = _executable,
                    UseShellExecute = true,
                };

                process.Start();

            }
            catch
            {

            }
        }
        #endregion

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

        private void AppIcon_Loaded(object sender, RoutedEventArgs e)
        {
            _defaultBrush = Background;
        }
    }
}
