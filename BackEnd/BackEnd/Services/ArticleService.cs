using BackEnd.Models;
using BackEnd.Requests;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services {

    public class ArticleService : IArticleService{
        private readonly ArticalContext _context;

        public ArticleService(ArticalContext _ctx) { _context = _ctx; }

        public async Task<Artical?> GetById(int _id) {
            return await _context.articles.FirstOrDefaultAsync(a => a.Id == _id);
        }

        public async Task<Artical> Create(ArticalRequest _req) {
            var article = new Artical {
                Title = _req.Title,
                Slug = _req.Slug ?? "unknown-slug",
                Content = _req.Content,
                Summary = _req.Summary ?? "unknown-summary",
                ImageUrl = _req.ImageUrl ?? "unknown-img",
                AuthorId = _req.AuthorId,
                CreatedTime = DateTime.UtcNow,
                IsPushiled = _req.IsPushiled
            };

            _context.articles.Add(article);
            await _context.SaveChangesAsync();

            return article;
        }
    }
}