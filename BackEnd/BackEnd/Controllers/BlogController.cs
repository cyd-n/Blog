using BackEnd.Models;
using BackEnd.Requests;
using BackEnd.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly ArticalContext _context;
        private readonly IArticleService _readOnlyArticleService;

        public BlogController(ArticalContext _ctx, IArticleService _articleService) { 
            _context = _ctx; 
            _readOnlyArticleService = _articleService;
        }

        [HttpGet("Artical/{_id}")]
        public async Task<IActionResult> GetArtical(int _id)
        {
            var article = await _context.articles.FirstOrDefaultAsync(m => m.Id == _id);

            if (article == null) { return NotFound(new { message = "Post not found" }); }

            var writer = await _context.users.FirstOrDefaultAsync(m => m.Id == article.AuthorId);

            if (writer == null) { return NotFound(new { message = "Author not found" }); }

            var category = await _context.catories.FirstOrDefaultAsync(m => m.Id == article.CatertoCategoryId);

            if (category == null) { return NotFound(new { message = "Category not found" }); }

            return Ok(new
            {
                title = article.Title ?? "Unkown-Title",
                slug = article.Slug ?? "Unkown-Slug",
                content = article.Content ?? "Unkown-Content",
                summary = article.Summary ?? "Unkown-Summary",
                img = article.ImageUrl ?? "Unkown-Img",

                author = new {
                    name = writer.Name ?? "Unkown-Author-Name",
                    email = writer.Email ?? "Unkown-Author-Email",
                    phone = writer.PhoneNumber ?? "Unkown-Author-Phone",

                    img = writer.ProfielImgUrl ?? "Unkown-Author-Img"
                },

                createDate = article.CreatedTime,
                UpdatedDate = article.LastUpdatedTime ?? DateTime.MinValue,
                PolishedDate = article.LastPolishedTime ?? DateTime.MinValue,
                DeleteDate = article.DeletedTime ?? DateTime.MinValue,

                isPublish = article.IsPushiled,

                category = new {
                    name = category.Name
                }
            });
        }
        
        [HttpGet("Articals")]
        public async Task<IActionResult> GetArticals() {
            var _articles = await _context.articles.ToListAsync();
            
            if (_articles == null) { return NotFound(new { message = "Posts not found" }); }

            return Ok(new {
                Articles = Enumerable.Range(0, _articles.Count).Select(i => new
                    {
                        Id = i,
                        Title = _articles[i].Title ?? "Unkown Title",
                        Summary = _articles[i].Summary ?? "Unkown Summary",
                        ImageUrl = _articles[i ].ImageUrl ?? "Unkown Img",
                        Author = _context.users.FirstOrDefault(m => m.Id == _articles[i].AuthorId),
                        Category = _context.catories.FirstOrDefault(m => m.Id == _articles[i].AuthorId)
                    })
            });
        }

        [HttpGet("User/{_id}")]
        public async Task<IActionResult> GetUserInfo(int _id)
        {
            var user = await _context.users.FirstOrDefaultAsync(m => m.Id == _id);

            if (user == null) { return NotFound(new { message = "User not found" }); }

            return Ok(new
            {
                name = user.Name,
                email = user.Email,
                phone = user.PhoneNumber ?? "Unkown-Phone",
                img = user.ProfielImgUrl ?? "Unkown-Img"
            });
        }

        [NonAction]
        public async Task<User?> AuthUser(int _id, string _name, string _pass)
        {
            var user = await _context.users.FirstOrDefaultAsync(u => u.Id == _id);

            if (_name == user.Name) return null;
            if (_pass == user.HashedPass) return null; 

            return user;
        }

        [NonAction]
        public async Task<IActionResult> GetLogIn(int _id, string _name, string _pass)
        {
            var user = await AuthUser(_id, _name, _pass);

            if (user == null) { return NotFound(new { message = "User is Invalid" }); }

            return Ok(new
            {
                id = user.Id,
                name = user.Name,
                pass = user.HashedPass,
                img = user.ProfielImgUrl ?? "Unkown"
            });
        }

        // POST api/<MatchController>
        [HttpPost("MakeCategory")]
        public async Task<IActionResult> MakeCategory([FromBody] CategoryRequest _req)
        {
            var category = new Category
            {
                Name = _req.Name,
                Slug = _req.Slug
            };

            _context.catories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new { id = category.Id, _req.Name, _req.Slug });
        }

        [HttpPost("MakeUser")]
        public async Task<IActionResult> MakeUser([FromBody] UserRequest _req)
        {
            // hash the password

            var user = new User
            {
                Name = _req.Name,
                Email = _req.Email ?? "Unkown-Email",
                PhoneNumber = _req.PhoneNumber,
                HashedPass = _req.HashedPass,
                ProfielImgUrl = _req.ProfielImgUrl ?? "Unkown-Img",
                CreatedTime = DateTime.UtcNow,
            };

            _context.users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(new { id = user.Id, user.Name, user.HashedPass });
        }

        [HttpPost("MakeArtical")]
        public async Task<IActionResult> MakeArtical([FromBody] ArticalRequest _req)
        {
            var article = await _readOnlyArticleService.Create(_req);
            return Ok(new { id = article.Id });
        }
        
        [HttpDelete("DeleteArtical/{_id}")]
        public async Task<IActionResult> DeleteArticle(int _id) {
            var _artical = await _context.articles.FindAsync(_id);

            if (_artical == null) { return NotFound(new { message = "Article not found" }); }
            
            _context.articles.Remove(_artical);
            await _context.SaveChangesAsync();
            
            return Ok(new { Message = $"Artical: {_id} is deleted" });
        }

        // Make Comment

        // User Login
    }
}
