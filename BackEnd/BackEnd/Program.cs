using BackEnd.Middleware;
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
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials()
                        .WithOrigins(
                            "http://localhost:5500",
                            "http://127.0.0.1:5500",
                            "http://localhost:5000",
                            "http://127.0.0.1:5000",
                            "http://localhost:3000",
                            "http://127.0.0.1:3000"
                        );
                });
            });

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ArticalContext>(options => {
                var conn = builder.Configuration.GetConnectionString("BlogDb");
                options.UseMySql(conn, ServerVersion.AutoDetect(conn));
            });

            builder.Services.AddScoped<IArticleService, ArticleService>();
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();
            
            app.UseCors("AllowFrontend");
            
            app.UseDeveloperExceptionPage();

           // if (app.Environment.IsDevelopment())
           // {
                app.UseSwagger();
                app.UseSwaggerUI();
           // }

            app.UseRouting();

            app.UseCors("AllowFrontend");
            
            app.UseMiddleware<RequestLoggingMiddleware>();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}



