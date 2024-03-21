using System;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace ElderlyCareApp.Controls
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
