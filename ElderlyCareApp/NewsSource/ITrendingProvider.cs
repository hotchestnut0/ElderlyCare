using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElderlyCareApp.NewsSource
{
    internal interface ITrendingProvider
    {
        bool Fetch();
        Task<bool> FetchAsync();
        Trending[] GetTrendings();
    }
}
