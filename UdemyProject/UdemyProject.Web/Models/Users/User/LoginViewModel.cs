using System.ComponentModel.DataAnnotations;

namespace UdemyProject.Web.Models.Users.User
{
    public class LoginViewModel
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
    }
}
