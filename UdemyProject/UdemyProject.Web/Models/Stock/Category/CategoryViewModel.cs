
namespace UdemyProject.Web.Models.Stock.Category
{
    public class CategoryViewModel
    {
        public CategoryViewModel(){}

        public int _id_category { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool condition { get; set; }
    }
}
