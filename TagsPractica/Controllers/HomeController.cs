using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TagsPractica.DAL.Repositories;
using TagsPractica.DAL.Models;
using TagsPractica.ViewModels;



namespace TagsPractica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;
        private IPostRepository _postRepository;

        public HomeController(ILogger<HomeController> logger, IPostRepository postRepository, IRoleRepository roleRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _postRepository = postRepository;
        }

        public async Task<IActionResult> Index(RegisterViewModel model)
        {
            // Добавим создание нового пользователя
            var newUser = new User()
            {
                Id = Guid.NewGuid(),
                userName = "Andrey",
                password = "Petrov",
                email = "1@1.ru",
                roleId = 3
                
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

            var DefaultPost = new Post()
            {
                title = "default",
                text = "dafault",
                //userId =  ConewUser.Id,

            };
            var DefaultComment = new Comment()
            {
                text = "dafault",
                //userId = newUser.Id,
                postId = 1
            };
            var DefaultTag = new Tag()
            {
                text = "default"
            };
            var DefaultPostTag = new PostTag()
            {
                postId = 1,
                tagId = 1
            };

            // Добавим в базу
            await _roleRepository.AddRole(newRole1);
            await _roleRepository.AddRole(newRole2);
            await _roleRepository.AddRole(newRole3);
            await _userRepository.AddUser(newUser);
            //await _postRepository.AddPost(DefaultPost);
            //await _postRepository.AddComment(DefaultComment);
            //await _postRepository.AddTag(DefaultTag);
            //await _postRepository.AddPostTag(DefaultPostTag);

            // Выведем результат
            Console.WriteLine($"User with id {newUser.Id}, named {newUser.userName} was successfully added on {newUser.email}");

            return View(model);
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