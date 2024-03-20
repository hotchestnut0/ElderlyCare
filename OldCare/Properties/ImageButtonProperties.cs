using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace OldCare.Properties
{
    internal class ImageButtonProperties
    {
        public static readonly DependencyProperty AppImageSourceProperty =
            DependencyProperty.RegisterAttached("AppImageSource", typeof(ImageSource), typeof(ImageButtonProperties));

        public static ImageSource GetAppImageSource(DependencyObject obj)
        {
            return (ImageSource)obj.GetValue(AppImageSourceProperty);
        }

        public static void SetAppImageSource(DependencyObject obj, ImageSource value)
        {
            obj.SetValue(AppImageSourceProperty, value);
        }
    }
}
