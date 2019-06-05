using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UdemyProject.Data;
using UdemyProject.Entities.Stock;
using UdemyProject.Web.Models.Stock.Category;

namespace UdemyProject.Web.Controllers
{
    [Authorize(Roles = "Stocker,Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly DBSystemContext _context;

        public CategoriesController(DBSystemContext context)
        {
            _context = context;
        }

        // GET: api/Categories/GetCategories
        [HttpGet("[action]")]
        public async Task <IEnumerable<CategoryViewModel>> GetCategories()
        {
            var category = await _context.Categories.ToListAsync();
            return category.Select(p=> new CategoryViewModel
            {
                _id_category = p._id_category,
                name = p.name,
                description = p.description,
                condition = p.condition
            });
        }
        // GET: api/Categories/GetCategoriesBasicInfo
        [HttpGet("[action]")]
        public async Task<IEnumerable<CategoryBasicInfoViewModel>> GetCategoriesBasicInfo()
        {
            var category = await _context.Categories.Where(c => c.condition == true).ToListAsync();
            return category.Select(p => new CategoryBasicInfoViewModel
            {
                _id_category = p._id_category,
                name = p.name
            });
        }

        // GET: api/Categories/getCategory/id
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> getCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _context.Categories.FindAsync(id);

            if (category == null)
                return NotFound();

            return Ok(new CategoryViewModel {
                _id_category = category._id_category,
                name = category.name,
                description = category.description,
                condition = category.condition
            });
        }

        // PUT: api/Categories/UpdateCategory
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryViewModel category_model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (category_model._id_category < 1)
                return BadRequest();

            var category = await _context.Categories.FirstOrDefaultAsync(c => c._id_category == category_model._id_category);

            if(category == null)
                return NotFound();
            category.name = category_model.name;
            category.description = category_model.description;

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

        // POST: api/Categories/RegisterNewCategory
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterNewCategory([FromBody] RegisterCategoryViewModel category_model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Category category = new Category
            {
                name = category_model.name,
                description = category_model.description,
                condition = true
            };

            _context.Categories.Add(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        // DELETE: api/Categories/DeleteCategory/id
        [HttpDelete("[action]/{id}")]
        public async Task<IActionResult> DeleteCategory([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return NotFound();

            _context.Categories.Remove(category);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch(Exception e)
            {
                return BadRequest();
            }

            return Ok(category);
        }

        // PUT: api/Categories/DesactivateCategory/id
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> DesactivateCategory([FromRoute] int id)
        {

            if (id < 1)
                return BadRequest();

            var category = await _context.Categories.FirstOrDefaultAsync(c => c._id_category == id);

            if (category == null)
                return NotFound();

            category.condition = false;
            //category.condition = !category.condition;

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

        // PUT: api/Categories/ActivateCategory/id
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> ActivateCategory([FromRoute] int id)
        {

            if (id < 1)
                return BadRequest();

            var category = await _context.Categories.FirstOrDefaultAsync(c => c._id_category == id);

            if (category == null)
                return NotFound();

            category.condition = true;
            //category.condition = !category.condition;

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

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e._id_category == id);
        }
    }
}