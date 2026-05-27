using BackEnd.Models;
using BackEnd.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;

namespace BackEnd {
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                });
            });

            builder.Services.AddDbContext<ArticalContext>(options =>
            {
                options.UseMySql(
                    "Server=127.0.0.1;Database=Blog;User=cydn;Password=F3nr!rsR0ar;",
                    ServerVersion.AutoDetect("Server=127.0.0.1;Database=Blog;User=cydn;Password=F3nr!rsR0ar;")
                );
            });

            builder.Services.AddScoped<IArticleService, ArticleService>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseCors();

            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}



