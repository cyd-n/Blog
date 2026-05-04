using Microsoft.EntityFrameworkCore;

namespace BackEnd.Models{
    public class ArticalContext : DbContext {
        public DbSet<Artical> articles { get; set; }

        public DbSet<Category> catories { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<Comment> comments { get; set; }

        public ArticalContext(DbContextOptions<ArticalContext> _options) : base(_options) { }
    }
}
