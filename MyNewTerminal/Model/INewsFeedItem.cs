using System;
using System.Text;
using System.Threading.Tasks;

namespace MyNewTerminal.Model
{
    public interface INewsFeedItem
    {
        string Author { get; set; }
        int NoOfComments { get; set; }
        int Id { get; set; }
        int[] CommentIds { get; set; }
        int Score { get; set; }
        int Timestamp { get; set; }
        string Title { get; set; }
        string Type { get; set; }
        string URL { get; set; }
        string URLBase { get; }
        void GetNewsFeed();
    }

    public abstract class NewsFeedItem : INewsFeedItem
    {      
        public string Author { get; set; }
        public int NoOfComments { get; set; }
        public int Id { get; set; }
        public int[] CommentIds { get; set; }
        public int Score { get; set; }
        public int Timestamp { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string URL { get; set; }
        public string URLBase { get; set; }
        public abstract void GetNewsFeed();
       
    }
}

