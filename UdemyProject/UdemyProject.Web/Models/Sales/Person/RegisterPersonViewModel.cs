﻿using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Web.Models.Sales.Person
{
    public class RegisterPersonViewModel
    {
        [Required]
        public string kind_of_person { get; set; }
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
    }
}
