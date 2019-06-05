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
using UdemyProject.Web.Models.Stock.Article;

namespace UdemyProject.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArticlesController : ControllerBase
    {
        private readonly DBSystemContext _context;

        public ArticlesController(DBSystemContext context)
        {
            _context = context;
        }

        // GET: api/Articles/GetArticles
        [Authorize(Roles = "Stocker,Admin")]
        [HttpGet("[action]")]
        public async Task<IEnumerable<ArticleViewModel>> GetArticles()
        {
            var article = await _context.Articles.Include(c => c.category).ToListAsync();
            return article.Select(p => new ArticleViewModel
            {
                _id_article = p._id_article,
                _id_category = p._id_category,
                category = p.category.name,
                code = p.code,
                name = p.name,
                stock = p.stock,
                price = p.price,
                description = p.description,
                condition = p.condition
            });
        }

        // GET: api/Article/GetArticle/id
        [Authorize(Roles = "Stocker,Admin")]
        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetArticle([FromRoute] int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var article = await _context.Articles.Include(c => c.category).SingleOrDefaultAsync(c=> c._id_article == id);

            if (article == null)
                return NotFound();

            return Ok(new ArticleViewModel
            {
                _id_article = article._id_article,
                _id_category = article._id_category,
                category = article.category.name,
                code = article.code,
                name = article.name,
                stock = article.stock,
                price = article.price,
                description = article.description,
                condition = article.condition
            });
        }

        // PUT: api/Articles/UpdateArticle
        [Authorize(Roles = "Stocker,Admin")]
        [HttpPut("[action]")]
        public async Task<IActionResult> UpdateArticle([FromBody] UpdateArticleViewModel article_model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (article_model._id_article < 1)
                return BadRequest();

            if (article_model._id_category < 1)
                return BadRequest();

            var article = await _context.Articles.FirstOrDefaultAsync(k => k._id_article == article_model._id_article);

            if (article == null)
                return NotFound();

            article._id_category = article_model._id_category;
            article.code = article_model.code;
            article.name = article_model.name;
            article.price = article_model.price;
            article.stock = article_model.stock;
            article.description = article_model.description;

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

        // POST: api/Articles/RegisterNewArticle
        [Authorize(Roles = "Stocker,Admin")]
        [HttpPost("[action]")]
        public async Task<IActionResult> RegisterNewArticle([FromBody] RegisterArticleViewModel article_model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Article article = new Article
            {
                _id_category = article_model._id_category,
                code = article_model.code,
                name = article_model.name,
                price = article_model.price,
                stock = article_model.stock,
                description = article_model.description,
                condition = true
            };

            _context.Articles.Add(article);

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return BadRequest();
            }

            return Ok();
        }

        // PUT: api/Categories/UpdateArticleStatus/id
        [Authorize(Roles = "Stocker,Admin")]
        [HttpPut("[action]/{id}")]
        public async Task<IActionResult> UpdateArticleStatus([FromRoute] int id)
        {

            if (id < 1)
                return BadRequest();

            var article = await _context.Articles.FirstOrDefaultAsync(k => k._id_article == id);

            if (article == null)
                return NotFound();

            article.condition = !article.condition;

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

        private bool ArticleExists(int id)
        {
            return _context.Articles.Any(e => e._id_article == id);
        }
    }
}