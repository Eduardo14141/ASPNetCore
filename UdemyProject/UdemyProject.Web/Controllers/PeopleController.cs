using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyProject.Data;
using UdemyProject.Entities.Sales;
using UdemyProject.Web.Models.Sales.Person;

namespace UdemyProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        private readonly DBSystemContext _context;

        public PeopleController(DBSystemContext context)
        {
            _context = context;
        }


        // GET: api/People/GetCustomers
        [Authorize(Roles = "Saler,Admin")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonViewModel>> GetCustomers()
        {
            var person = await _context.People.Where(p => p.kind_of_person == "Cliente").ToListAsync();
            return person.Select(p => new PersonViewModel
            {
                _id_person = p._id_person,
                kind_of_person = p.kind_of_person,
                name = p.name,
                kind_of_document = p.kind_of_document,
                document_number = p.document_number,
                adress = p.adress,
                telephone = p.telephone,
                email = p.email
            });
        }

        // GET: api/People/GetSuppliers
        [Authorize(Roles = "Stocker,Admin")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<PersonViewModel>> GetSuppliers()
        {
            var person = await _context.People.Where(p => p.kind_of_person == "Proveedor").ToListAsync();
            return person.Select(p => new PersonViewModel
            {
                _id_person = p._id_person,
                kind_of_person = p.kind_of_person,
                name = p.name,
                kind_of_document = p.kind_of_document,
                document_number = p.document_number,
                adress = p.adress,
                telephone = p.telephone,
                email = p.email
            });
        }

        // POST: api/Users/RegisterPerson
        [Authorize(Roles = "Stocker,Saler,Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterPerson([FromBody] RegisterPersonViewModel person_model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var email = person_model.email.ToLower();
            if (await _context.People.AnyAsync(e => e.email == email))
                return BadRequest("El email ya está registrado");


            Person person = new Person
            {
                kind_of_person = person_model.kind_of_person,
                name = person_model.name,
                kind_of_document = person_model.kind_of_document,
                document_number = person_model.document_number,
                adress = person_model.adress,
                telephone = person_model.telephone,
                email = person_model.email
            };

            _context.People.Add(person);

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

        // PUT: api/People/UpdatePerson
        [Authorize(Roles = "Stocker,Saler,Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdatePerson([FromBody] UpdatePersonViewModel person_model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (person_model._id_person < 1)
                return BadRequest();

            var person = await _context.People.FirstOrDefaultAsync(k => k._id_person == person_model._id_person);

            if (person == null)
                return NotFound();
            person.kind_of_person = person_model.kind_of_person;
            person.name = person_model.name;
            person.kind_of_document = person_model.kind_of_document;
            person.document_number = person_model.document_number;
            person.telephone = person_model.telephone;
            person.adress = person_model.adress;
            person.email = person_model.email;

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

        private bool PersonExists(int id)
        {
            return _context.People.Any(e => e._id_person == id);
        }
    }
}