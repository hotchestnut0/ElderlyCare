using System;
using System.Globalization;
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
                Dispatcher.Invoke(() =>
                {
                    DateDisplay.Text = DateTime.Now.ToString("yyyy 年 MM 月 dd 日");
                    ChineseDateText.Text = GetChineseCalenderString(DateTime.Now);
                });
                
                await Task.Delay(TimeSpan.FromSeconds(5));
            }
        }

        private string GetChineseCalenderString(DateTime dateTime)
        {
            System.Globalization.ChineseLunisolarCalendar cal = new System.Globalization.ChineseLunisolarCalendar();

            int year = cal.GetYear(dateTime);
            int month = cal.GetMonth(dateTime);
            int day = cal.GetDayOfMonth(dateTime);
            int leapMonth = cal.GetLeapMonth(year);
            return string.Format("农历{0}{1}（{2}）年{3}{4}月{5}{6}"
                , "甲乙丙丁戊己庚辛壬癸"[(year - 4) % 10]
                , "子丑寅卯辰巳午未申酉戌亥"[(year - 4) % 12]
                , "鼠牛虎兔龙蛇马羊猴鸡狗猪"[(year - 4) % 12]
                , month == leapMonth ? "闰" : ""
                , "无正二三四五六七八九十冬腊"[leapMonth > 0 && leapMonth <= month ? month - 1 : month]
                , "初十廿三"[day / 10]
                , "日一二三四五六七八九"[day % 10]
            );
        }
    }
}
