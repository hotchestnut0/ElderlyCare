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

namespace OldCare.Controls
{
    /// <summary>
    /// SimpleCalender.xaml 的交互逻辑
    /// </summary>
    public partial class SimpleCalender : UserControl
    {
        public SimpleCalender()
        {
            InitializeComponent();
            _ = UpdateDate();
        }

        private async Task UpdateDate()
        {
            while (true)
            {
                DateDisplay.Text = DateTime.Now.ToString("yyyy 年 MM 月 dd 日");
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }
    }
}
