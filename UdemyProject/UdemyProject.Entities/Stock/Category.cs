using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Entities.Stock
{
    public class Category
    {
        public int _id_category { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Zá-úÁ-ÚñÑüÜ]{3,50}$", ErrorMessage = "La categoría debe estar escrita en el alfabeto español y tener entre 3 y 50 caracteres")]
        public string name { get; set; }
        [StringLength(256, ErrorMessage = "La descripción debe tener como máximo 256 caracteres")]
        public string description { get; set; }
        public bool condition { get; set; }

        public ICollection<Article> articles { get; set; }
    }
}
