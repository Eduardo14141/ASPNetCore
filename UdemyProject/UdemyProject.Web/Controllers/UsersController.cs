using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UdemyProject.Data;
using UdemyProject.Entities.Users;
using UdemyProject.Web.Models.Users.User;

namespace UdemyProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DBSystemContext _context;
        private readonly IConfiguration _config;
        public UsersController(DBSystemContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        // GET: api/Users/GetUsers
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<UserViewModel>> GetUsers()
        {
            var user = await _context.Users.Include(p => p.rol).ToListAsync();
            return user.Select(p => new UserViewModel
            {
                _id_user = p._id_user,
                _id_rol = p._id_rol,
                rol = p.rol.name,
                name = p.name,
                kind_of_document = p.kind_of_document,
                document_number = p.document_number,
                adress = p.adress,
                telephone = p.telephone,
                email = p.email,
                password_hash = p.password_hash,
                condition = p.condition
            });
        }

        // POST: api/Users/RegisterNewUser
        [Authorize(Roles = "Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterNewUser([FromBody] RegisterUserViewModel user_model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = user_model.email.ToLower();
            if (await _context.Users.AnyAsync(e => e.email == email))
                return BadRequest("El email ya está registrado");

            Utils.Utils.EncryptPassword(user_model.password, out byte[] password_hash, out byte[] password_salt);

            User user = new User
            {
                _id_rol = user_model._id_rol,
                name = user_model.name,
                kind_of_document = user_model.kind_of_document,
                document_number = user_model.document_number,
                adress = user_model.adress,
                telephone = user_model.telephone,
                email = user_model.email.ToLower(),
                password_hash = password_hash,
                password_salt = password_salt,
                condition = true
            };

            _context.Users.Add(user);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }

            return Ok();
        }

        // PUT: api/Users/UpdateUser
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserViewModel user_model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (user_model._id_rol < 1)
                return BadRequest();

            if (user_model._id_user < 1)
                return BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(k => k._id_user == user_model._id_user);

            if (user == null)
                return NotFound();

            user._id_rol = user_model._id_rol;
            user.kind_of_document = user_model.kind_of_document;
            user.document_number = user_model.document_number;
            user.telephone = user_model.telephone;
            user.adress = user_model.adress;
            user.email = user_model.email;
            user.name = user_model.name;

            if (user_model.update_password)
            {
                UdemyProject.Web.Utils.Utils.EncryptPassword(user_model.password, out byte[] hash, out byte[] salt);
                user.password_hash = hash;
                user.password_salt = salt;
            }


            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Users/UpdateUserStatus/id
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateUserStatus([FromRoute] int id)
        {

            if (id < 1)
                return BadRequest();

            var user = await _context.Users.FirstOrDefaultAsync(k => k._id_user == id);

            if (user == null)
                return NotFound();

            user.condition = !user.condition;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest();
            }

            return Ok();
        }

        [HttpPost ("[action]")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var email = model.email.ToLower();
            var user = await _context.Users.Where(u=> u.condition).Include(u => u.rol).FirstOrDefaultAsync(u => u.email == email);
            if (user == null)
                return NotFound();
            if (!Utils.Utils.VerifyLogin(model.password, user.password_hash, user.password_salt))
                return NotFound();
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user._id_user.ToString()),
                new Claim(ClaimTypes.Email, email),
                new Claim(ClaimTypes.Role, user.rol.name),
                new Claim("_id_user", user._id_user.ToString()),
                new Claim("name", user.name),
                new Claim("rol", user.rol.name)
            };

            return Ok( new { token = Utils.Utils.generateLoginToken(claims, _config) } );
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e._id_user == id);
        }
    }
}