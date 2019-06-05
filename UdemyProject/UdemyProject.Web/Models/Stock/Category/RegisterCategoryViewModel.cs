using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Web.Models.Stock.Category
{
    public class RegisterCategoryViewModel
    {
        [Required]
        [RegularExpression(@"^[a-zA-Zá-úÁ-ÚñÑüÜ ]{3,50}$", ErrorMessage = "Tu nombre debe estar escrito en el alfabeto español y tener entre 3 y 50 caracteres")]
        public string name { get; set; }
        [RegularExpression(@"^[a-zA-Zá-úÁ-ÚñÑüÜ \.\:\-]{0,256}$", ErrorMessage = "La descripción debe tener como máximo 256 caracteres")]
        public string description { get; set; }
    }
}
