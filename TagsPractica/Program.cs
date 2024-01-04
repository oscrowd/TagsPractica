using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagsPractica.DAL;
using TagsPractica.DAL.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using NLog;

namespace TagsPractica
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            //string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
            string? connectionSQL = builder.Configuration.GetConnectionString("DefaultConnection");
            // Add services to the container.
            var mapperConfig = new MapperConfiguration((v) =>
            {
                v.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();

            builder.Services.AddSingleton(mapper);
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

            Logger logger = LogManager.GetCurrentClassLogger();
            logger.Trace("trace message");
            logger.Debug("debug message");
            logger.Warn("warn message");
            logger.Error("error message");
            logger.Fatal("fatal message");
            logger.Info("info message");

            builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseAuthentication();
            app.UseStatusCodePagesWithRedirects("/Error/Default?statusCode={0}");
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }

    }
}
