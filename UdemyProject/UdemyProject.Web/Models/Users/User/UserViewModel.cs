
namespace UdemyProject.Web.Models.Users.User
{
    public class UserViewModel
    {
        public int _id_user { get; set; }
        public int _id_rol { get; set; }
        public string rol { get; set; }
        public string name { get; set; }
        public string kind_of_document { get; set; }
        public string document_number { get; set; }
        public string adress { get; set; }
        public string telephone { get; set; }
        public string email { get; set; }
        public byte[] password_hash { get; set; }
        public bool condition { get; set; }
    }
}
