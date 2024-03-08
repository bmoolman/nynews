using MyNewTerminal.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace MyNewTerminal
{

    class Program
    {
        private static List<INewsFeedItem> _itemList;

        public static void Main(string[] args)
        {
            Console.WriteLine("Starting connections");
            Console.WriteLine("Press R to read the newsfeed");

            string line;
            do
            {
                line = Console.ReadLine();
                if (line != null && line == "R")
                {
                    Console.ResetColor();
                    LoadLists();
                }
                else
                {
                    var isNumeric = int.TryParse(line, out int n);

                    if (n > 0)
                    {
                        Console.WriteLine("loading in browser...");
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
            myProcess.StartInfo.FileName = $"{_itemList[postNo - 1].URL}";
            myProcess.Start();
        }

        public static void LoadLists()
        {
            Console.Clear();
            Console.WriteLine("Loading data from ycombinator");
            _itemList = NewsFactory.CreateNewsItemsFor(NewsFactory.NewsStoreTypes.HackerNews,true);

            ShowItemsInList();

         

        }


        public static void ShowItemsInList()
        {
            Console.ResetColor();
            int itemCounter = 0;
            string spaces;
            foreach (var item in _itemList)
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
                Console.Write($"{item.Title}");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($" ({item.URLBase})\n");
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($"\t{item.Score} points by {item.Author} | {item.NoOfComments} comments\n");
                //Console.WriteLine($"{item.url}");
                //Console.WriteLine();
            }
            Console.BackgroundColor = ConsoleColor.Blue;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Choose an item [1-20] or R to refresh");

        }


    }

}
