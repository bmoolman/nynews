using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyNewTerminal.Model
{
    public class NewsItem
    {
        public string by;
        public int descendants;
        public int id;
        public int[] kids;
        public int score;
        public int time;
        public string title;
        public string type;
        public string url;
        public string urlBase => new Uri(url).Host;
    }

    public class DefaultNewsFeed : NewsFeedItem
    {
        public override void GetNewsFeed()
        {
            throw new NotImplementedException();
        }
    }

    public class YCombinatorNewsFeed : NewsFeedItem
    {
        private static HttpClient Client;
        private List<int> _itemList;
        private List<NewsItem> _newsItems;
        private int _startIndex;
        private int _endIndex;
        private int _incrementIndexBy = 10;
        public List<INewsFeedItem> NewsItems
        {
            get
            {
                List<INewsFeedItem> newsFeedItems = new List<INewsFeedItem>();
                foreach (var item in _newsItems)
                {
                    if (item.url == null)
                    {
                        item.url = "https://news.ycombinator.com/";
                    }
                    newsFeedItems.Add(new DefaultNewsFeed()
                    {
                        Author = item.by,
                        NoOfComments = item.descendants,
                        CommentIds = item.kids,
                        Title = item.title,
                        Timestamp = item.time,
                        Type = item.type,
                        Score = item.score,
                        URL = item.url,
                        URLBase = new Uri(item.url).Host
                    }) ;
                }
                
                return newsFeedItems;
            }
        }

      

        public YCombinatorNewsFeed(bool reloadFeed, int startIndex, int endIndex)
        {
            _startIndex = startIndex;
            _endIndex = endIndex;

            if (Client==null)
            {
                Client = new HttpClient();
                GetNewsFeed();               
            }

            if (reloadFeed==true)
            {
                GetNewsFeed();
            }
        }

        public override void GetNewsFeed()
        {
            var task_response  = Client.GetAsync("https://hacker-news.firebaseio.com/v0/topstories.json");
            var response  = task_response.Result;
            response.EnsureSuccessStatusCode();

            var task_content = response.Content.ReadAsStringAsync();
            string content = task_content.Result;

            _itemList = JsonConvert.DeserializeObject<List<int>>(content);
            _newsItems = new List<NewsItem>();
            for (int i = _startIndex-1; i < _endIndex; i++)
            {
                var task_newsFeeItem = Client.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{_itemList[i]}.json");
                var newsFeedItem = task_newsFeeItem.Result;
                var newsItemCont = JsonConvert.DeserializeObject<NewsItem>(newsFeedItem);
                _newsItems.Add(newsItemCont);
            }

        }
    }
}

