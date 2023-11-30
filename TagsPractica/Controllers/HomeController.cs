using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TagsPractica.DAL.Repositories;
using TagsPractica.Models;

namespace TagsPractica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            
        }

        public async Task<IActionResult> Index()
        {
            // Добавим создание нового пользователя
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                userName = "Andrey",
                password = "Petrov",
                email = "1@1.ru"
                // = DateTime.Now
            };

            var newRole1 = new Role()
            {
                roleName = "Admin"
            };
            var newRole2 = new Role()
            {
               roleName = "Moderator"
            };
            var newRole3 = new Role()
            {
                roleName = "User"
            };

            // Добавим в базу
            //await _userRepository.AddUser(newUser);
            await _roleRepository.AddRole(newRole1);
            await _roleRepository.AddRole(newRole2);
            await _roleRepository.AddRole(newRole3);
            // Выведем результат
            //Console.WriteLine($"User with id {newUser.Id}, named {newUser.userName} was successfully added on {newUser.email}");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}