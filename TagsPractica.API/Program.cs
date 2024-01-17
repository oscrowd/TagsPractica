using Microsoft.OpenApi.Models;
using AutoMapper;
using TagsPractica.API.Contracts;
using TagsPractica.DAL.Repositories;
using TagsPractica.DAL.Models;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using TagsPractica.DAL;



namespace TagsPractica.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SomeNewsBlog API",
                    Description = "News Blog created by Mellomanprost",
                });
                var basePath = AppContext.BaseDirectory;

                var xmlPath = Path.Combine(basePath, "TagsPractica.API.xml");
                //options.IncludeXmlComments(xmlPath);
            });

            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services.AddSingleton(mapper);
            //var mapper = mapperConfig.CreateMapper();
            //var assembly = Assembly.GetAssembly(typeof(MappingProfile));

            string? connectionSQL = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddSingleton<IUserRepository, UserRepository>();
            builder.Services.AddSingleton<IRoleRepository, RoleRepository>();
            builder.Services.AddSingleton<IPostRepository, PostRepository>();
            builder.Services.AddSingleton<ITagRepository, TagRepository>();
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite(connectionSQL), ServiceLifetime.Singleton);

            //Аутентификация
            builder.Services.AddAuthentication(options => options.DefaultScheme = "Cookies")
                .AddCookie("Cookies", options =>
                {
                    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
                    {
                        OnRedirectToLogin = redirectContext =>
                        {
                            redirectContext.HttpContext.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        }
                    };
                });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();
            app.UseAuthentication();
            app.MapControllers();

            app.Run();
        }
    }
}
