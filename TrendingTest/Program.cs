using ElderlyCareApp.NewsSource;

namespace TrendingTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            BaiduTrendingProvider newsHelper = new();
            await newsHelper.FetchAsync();

            foreach (var i in newsHelper.GetTrendings())
            {
                Console.WriteLine($"{i.No} {i.Title}");
            }

            Console.ReadLine();
        }
    }
}
