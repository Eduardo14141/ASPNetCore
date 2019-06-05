
namespace UdemyProject.Web.Models.Stock.Article
{
    public class ArticleViewModel
    {
        public int _id_article { get; set; }
        public int _id_category { get; set; }
        public string category { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public int stock { get; set; }
        public string description { get; set; }
        public bool condition { get; set; }

    }
}
