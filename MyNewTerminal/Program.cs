using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyNewTerminal
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

   
    public class Record
    {
        public NewsItem record;
    }

    class Program
    {
        private static HttpClient Client = new HttpClient();
        private static List<int> _itemList;
        private static List<NewsItem> _newsItems;
        public static async Task Main(string[] args)
        {
            Console.WriteLine("Starting connections");

            string line;
            do
            {
                line = Console.ReadLine();
                if (line != null && line == "R")
                    await LoadLists();
                else
                {
                    var isNumeric = int.TryParse(line, out int n);

                    if (n > 0)
                    {
                        LoadPost(n);
                    }
                    else
                    {
                        Console.WriteLine(line);
                    }
                   ;
                }
            } while (line != null);



        }

        public static void LoadPost(int postNo)
        {
            Process myProcess = new Process();
            myProcess.StartInfo.UseShellExecute = true;
            myProcess.StartInfo.FileName = $"{_newsItems[postNo - 1].url}";
            myProcess.Start();
        }



        public static async Task LoadLists()
        {
            Console.Clear();
            Console.WriteLine("Loading data from ycombinator");

            var result = await Client.GetStringAsync("https://hacker-news.firebaseio.com/v0/topstories.json");

            _itemList = JsonConvert.DeserializeObject<List<int>>(result);
            _newsItems = new List<NewsItem>();
            for (int i = 1; i < 15; i++)
            {
                var newsItem = await Client.GetStringAsync($"https://hacker-news.firebaseio.com/v0/item/{_itemList[i]}.json");
                var newsItemCont = JsonConvert.DeserializeObject<NewsItem>(newsItem);
                _newsItems.Add(newsItemCont);
            }

            ShowItemsInList();

        }


        public static void ShowItemsInList()
        {
            int itemCounter = 0;
            string spaces;
            foreach (var item in _newsItems)
            {
                                itemCounter++;

                if (itemCounter < 10)
                {
                    spaces = "  ";
                }
                else
                {
                    spaces = " ";
                }
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"[{itemCounter}]{spaces}");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"{item.title}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" ({item.urlBase})\n");
                Console.ForegroundColor = ConsoleColor.DarkGray;              
                Console.Write($"\t{item.score} points by {item.by} | {item.descendants} comments\n");
                //Console.WriteLine($"{item.url}");
                //Console.WriteLine();
            }
            Console.WriteLine("Choose an item [1-20] or R to refresh");
        }


    }

}
