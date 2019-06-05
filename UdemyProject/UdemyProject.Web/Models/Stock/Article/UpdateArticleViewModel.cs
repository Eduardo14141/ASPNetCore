using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Web.Models.Stock.Article
{
    public class UpdateArticleViewModel
    {
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Datos inválidos")]
        public int _id_article { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage ="Datos inválidos")]
        public int _id_category { get; set; }
        [RegularExpression(@"^[0-9a-zA-Zá-úÁ-ÚñÑüÜ \.]+$", ErrorMessage = "El código sólo puede tener letras, números y puntos")]
        public string code { get; set; }
        [RegularExpression(@"(^()$)|(^[a-zA-Zá-úÁ-ÚñÑüÜ0-9]{3,50}$)", ErrorMessage = "Tu nombre debe estar escrito en el alfabeto español y tener entre 3 y 50 caracteres")]
        public string name { get; set; }
        [Required]
        [RegularExpression(@"^([0-9]{1,6})((\.{0,1})([0-9]{1,2})){0,1}$", ErrorMessage = "El precio es un número con máximo 2 decimales")]
        public decimal price { get; set; }
        [Required]
        [RegularExpression(@"^[0-9]{1,5}$", ErrorMessage = "El inventario se expresa mediante números enteros")]
        public int stock { get; set; }
        [RegularExpression(@"^[a-zA-Zá-úÁ-ÚñÑüÜ0-9 \.\:\-]{0,256}$", ErrorMessage = "La descripción debe tener como máximo 256 caracteres")]
        public string description { get; set; }
    }
}
