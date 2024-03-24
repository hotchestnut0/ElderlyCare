using ElderlyCareApp.Utils;

namespace TrendingTest
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            NewsHelper newsHelper = new();
            await newsHelper.FetchAsync();

            foreach (var i in newsHelper.GetTrends())
            {
                Console.WriteLine($"{i.No} {i.Title}");
            }

            Console.ReadLine();
        }
    }
}
