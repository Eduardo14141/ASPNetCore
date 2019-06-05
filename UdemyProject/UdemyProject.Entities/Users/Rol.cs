using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Entities.Users
{
    public class Rol
    {
        public int _id_rol { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Zá-úÁ-ÚñÑüÜ0-9]{3,30}$", ErrorMessage = "El rol debe tener entre 3 y 30 caracteres")]
        public string name { get; set; }
        [RegularExpression(@"^[a-zA-Zá-úÁ-ÚñÑüÜ0-9 \.\:\-]{0,100}$", ErrorMessage = "La descripción debe tener como máximo 256 caracteres")]
        public string description { get; set; }
        public bool condition { get; set; }

        public ICollection<User> Users { get; set; }
    }
}
