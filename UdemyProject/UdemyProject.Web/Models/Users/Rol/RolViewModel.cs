using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UdemyProject.Web.Models.Users.Rol
{
    public class RolViewModel
    {
        public int _id_rol { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool condition { get; set; }
    }
}
