using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TagsPractica.DAL;
using TagsPractica.DAL.Repositories;



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
            builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionSQL), ServiceLifetime.Singleton);
            
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();

        }

    }
}
