﻿using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Web.Models.Users.User
{
    public class UpdateUserViewModel
    {
        [Required]
        public int _id_user { get; set; }
        [Required]
        public int _id_rol { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Zá-úÁ-ÚñÑüÜ0-9 ]{3,100}$", ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres")]
        public string name { get; set; }
        public string kind_of_document { get; set; }
        public string document_number { get; set; }
        public string adress { get; set; }
        [RegularExpression(@"(^[0-9]{8}$)|(^[0-9]{10}$)", ErrorMessage = "Ingrese un teléfono de 8 o 10 dígitos")]
        public string telephone { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public bool update_password { get; set; }
    }
}
