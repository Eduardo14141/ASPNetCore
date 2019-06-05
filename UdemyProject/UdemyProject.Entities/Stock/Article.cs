using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Entities.Stock
{
    public class Article
    {
        [Required]
        public int _id_article { get; set; }
        [Required]
        public int _id_category { get; set; }
        public string code { get; set; }
        [RegularExpression(@"(^()$)|(^[a-zA-Zá-úÁ-ÚñÑüÜ0-9]{3,50}$)", ErrorMessage = "Tu nombre debe estar escrito en el alfabeto español y tener entre 3 y 50 caracteres")]
        public string name { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{1,6})((\.{0,1})([0-9]{1,2})){0,1}$")]
        public decimal price { get; set; }
        [Required]
        [RegularExpression(@"[0-9]{1,5}$")]
        public int stock { get; set; }
        [RegularExpression(@"^[a-zA-Zá-úÁ-ÚñÑüÜ0-9 \.\:\-]{0,256}$", ErrorMessage = "La descripción debe tener como máximo 256 caracteres")]
        public string description { get; set; }
        public bool condition { get; set; }

        public Category category { get; set; }
    }
}
