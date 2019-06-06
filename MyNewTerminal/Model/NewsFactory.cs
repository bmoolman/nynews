using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyNewTerminal.Model
{

    public static class NewsFactory
    {
        public enum NewsStoreTypes
        {
            HackerNews,
            Reddit_NotImplementedYet 
        }
        public static List<INewsFeedItem> CreateNewsItemsFor(NewsStoreTypes type, bool reloadFeed)
        {
            switch (type)
            {
                case NewsStoreTypes.HackerNews:                                     
                    return new YCombinatorNewsFeed(reloadFeed).NewsItems;
                default:
                    return null;
            }
        }
    }
}