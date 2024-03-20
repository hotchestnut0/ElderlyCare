using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace OldCare.Properties
{
    class SettingsProperties : DependencyObject
    {
        public static SettingsProperties SettingsInstance { get; } = new();

        public static readonly DependencyProperty FontSizeSettings =
            DependencyProperty.RegisterAttached(nameof(FontSizeRaw), typeof(double), typeof(SettingsProperties), new(20d));

        public double FontSizeRaw
        {
            get => (double)GetValue(FontSizeSettings);
            set
            {
                double v = value;
                if (v is < 16 or > 30)
                    v = 20;
                SetValue(FontSizeSettings, v);
            }
        }
    }
}
