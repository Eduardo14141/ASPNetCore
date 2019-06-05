using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyProject.Data;
using UdemyProject.Entities.Users;
using UdemyProject.Web.Models.Users.Rol;

namespace UdemyProject.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class RolsController : ControllerBase
    {
        private readonly DBSystemContext _context;

        public RolsController(DBSystemContext context)
        {
            _context = context;
        }

        // GET: api/Rols
        [HttpGet]
        public IEnumerable<Rol> GetRols()
        {
            return _context.Rols;
        }

        // GET: api/Rols/GetAllRols
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolViewModel>> GetAllRols()
        {
            var rol = await _context.Rols.ToListAsync();
            return rol.Select(p => new RolViewModel
            {
                _id_rol = p._id_rol,
                name = p.name,
                description = p.description,
                condition = p.condition
            });
        }

        // GET: api/Rols/GetRolsBasicInfo
        [HttpGet("[action]")]
        public async Task<IEnumerable<RolBasicInfoViewModel>> GetRolsBasicInfo()
        {
            var rol = await _context.Rols.Where(r => r.condition == true).ToListAsync();
            return rol.Select(p => new RolBasicInfoViewModel
            {
                _id_rol = p._id_rol,
                name = p.name
            });
        }

        private bool RolExists(short id)
        {
            return _context.Rols.Any(e => e._id_rol == id);
        }
    }
}