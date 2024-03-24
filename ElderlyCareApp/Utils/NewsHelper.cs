using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace ElderlyCareApp.Utils
{
    public class NewsHelper
    {
        private const string _trendPage = "https://top.baidu.com/board?tab=realtime";
        private List<Trending> trendings = new();

        bool _navigationCompleted = false;

        public NewsHelper()
        {

        }

        public async Task<bool> FetchAsync()
        {
            try
            {
                HtmlWeb web = new HtmlWeb();
                HtmlDocument doc = await web.LoadFromWebAsync(_trendPage);


                await Task.Run(() => AnalyzeHtml(doc));
                return true;
            }
            catch { return false; }
        }

        public Trending[] GetTrends()
        {
            return trendings.ToArray();
        }

        private void AnalyzeHtml(HtmlDocument doc)
        {
            var htmlNodes = doc.DocumentNode.SelectNodes("//*[@id=\"sanRoot\"]/main/div[2]/div/div[2]/div[@class=\"category-wrap_iQLoo horizontal_1eKyQ\"]");
            int id = 0;
            foreach (var i in htmlNodes)
            {
                if (i == null)
                    continue;
                var contentNode = i.SelectSingleNode("./div[@class='content_1YWBm']");
                if (contentNode == null)
                    continue;

                var linkNode = contentNode.SelectSingleNode("./a");
                var titleNode = linkNode.SelectSingleNode("./div[@class='c-single-text-ellipsis']");
                var descriptionNode = contentNode.SelectSingleNode("./div/text()[not(ancestor::a)]");
                var imageNode = i.SelectSingleNode("./a/img");

                Trending trending = new Trending();
                trending.No = id++;
                trending.Link = linkNode.Attributes["href"].Value;
                trending.Title = titleNode.InnerText;
                trending.ImageUrl = imageNode.Attributes["src"].Value;
                trending.Description = descriptionNode.InnerText;

                trendings.Add(trending);
            }
        }
    }

    public struct Trending
    {

        public int No { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string ImageUrl { get; set; }
    }
}
