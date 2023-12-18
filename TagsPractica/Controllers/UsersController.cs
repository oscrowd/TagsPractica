using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TagsPractica.DAL;
using TagsPractica.ViewModels;
using System.Security.Authentication;
using TagsPractica.DAL.Repositories;
using TagsPractica.DAL.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol.Plugins;
using Azure.Core;

namespace TagsPractica.Controllers
{
    public class UsersController : Controller
    {
        private IMapper _mapper;
        private readonly DatabaseContext _context;
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public UsersController(IMapper mapper, DatabaseContext context, IUserRepository userRepository, IRoleRepository roleRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
        }

        // GET: Users
        [HttpGet]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index(string message)
        {
            ViewData["Message"] = message;
            return View();            
                          
            
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,userName,password,email")] User user)
        {
            if (ModelState.IsValid)
            {
                user.Id = Guid.NewGuid();
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(user);
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            ViewBag.roleId = new SelectList(_context.Roles, "Id", "roleName");
            ViewBag.Categories = _context.Roles.ToList();
            return View();
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, EditViewModel model)
        {
            Guid guid;
            string mess;
            Guid.TryParse(model.Id, out guid);
            var dbUser = _userRepository.GetById(guid);
            
            User user = new User();
            user = dbUser.Result;
            if (user != null && ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(model.userName))
                    user.userName = model.userName;
                if (!string.IsNullOrEmpty(model.email))
                    user.email = model.email;
                if (!string.IsNullOrEmpty(model.password))
                    user.password = model.password;
                if (model.roleId > 0)
                    user.roleId = model.roleId;

                _context.SaveChanges();
                mess = ($"User, named {user.userName} was successfully updated");
                return RedirectToAction("Index", new { message = mess });
            }
            else
            {
                mess = ($"Данные не валидны или не найден пользователь");
                ViewData["Message"] = mess;
                ViewBag.roleId = new SelectList(_context.Roles, "Id", "roleName");
                ViewBag.Categories = _context.Roles.ToList();
                return View(model);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Find()
        {
            return View();
        }

        // GET: Users/Edit/5
        [HttpPost]
        public async Task<IActionResult> Find(Guid id)
        {
            var user = await _userRepository.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            ViewViewModel model = new ViewViewModel();
            model = _mapper.Map<ViewViewModel>(user);
            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPatch]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(Guid id, [Bind("Id,userName,password,email")] EditViewModel model)
        {
            Guid guid;
            Guid.TryParse(model.Id, out guid);
            var user = _userRepository.GetById(guid);
            

            if (id != guid)
            {
                return NotFound();
            }
            try
            {
                _context.Update(user);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(guid))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(EditViewModel model)
        {
           if (model == null)
            {
                return NotFound();
            }

            Guid guid;
            Guid.TryParse(model.Id, out guid);
            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.Id == guid);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'DatabaseContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(Guid id)
        {
          return (_context.Users?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private bool UserExistsName(string userName)
        {
            return (_context.Users?.Any(e => e.userName == userName)).GetValueOrDefault();
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {

            // Добавим в базу
            await _userRepository.AddUser(user);
            // Выведем результат
            Console.WriteLine($"User with id {user.Id}, named {user.userName} was successfully added on {user.email}");
            return View(user);
        }

        [HttpGet]
        public IActionResult Register2()
        {
            ViewBag.roleId = new SelectList(_context.Roles, "Id", "roleName");
            ViewBag.Categories = _context.Roles.ToList();
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Register2(RegisterViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var user = _mapper.Map<User>(model);
                await _userRepository.AddUser(user);
                // Выведем результат
                string mess = ($"User, named {user.userName} was successfully added");
                ViewData["Message"] = mess;
                return RedirectToAction("Index", new { message = mess });

            }
            else
            {
                ViewData["Message"] = "Регистрация не прошла. Заполнте все поля и правильность паролей";
                ViewBag.roleId = new SelectList(_context.Roles, "Id", "roleName");
                ViewBag.Categories = _context.Roles.ToList();
                return View(model);
            }
        }


        [HttpGet]
        public IActionResult  Authenticate()
        {
            return View();
        }


        [HttpPost]
        //[Route("authenticate")]
        public IActionResult Authenticate(AuthViewModel model)
        {
            bool exist;
            if (String.IsNullOrEmpty(model.userName) || String.IsNullOrEmpty(model.password))
            {
                exist = false;

                return View(model);
            }
            exist = _userRepository.GetByLogin(model.userName);


            if (exist)
            {
                return RedirectToAction(nameof(HomeController.Index));
            }
            else
            {
                return View(model);
            }

        }


        [HttpGet]
        public IActionResult Authenticate2()
        {
            return View();
        }

        [HttpPost]
        //[Route("authenticate")]
        public async Task<IActionResult> Authenticate2(AuthViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                if (String.IsNullOrEmpty(model.userName) ||
                String.IsNullOrEmpty(model.password))
                    throw new ArgumentNullException("Запрос не корректен");



                user = _userRepository.GetByLogin2(model.userName, model.password);

                bool exist = _userRepository.GetByLogin(model.userName);

                if (exist)
                {
                    string roleName = _roleRepository.GetById(user.roleId);
                    user.Role.roleName = roleName;
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimsIdentity.DefaultNameClaimType, user.userName),
                        new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.roleName),

                    };
                    ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                        claims,
                        "AppCookie",
                        ClaimsIdentity.DefaultNameClaimType,
                        ClaimsIdentity.DefaultRoleClaimType);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
                    model = _mapper.Map<AuthViewModel>(user);
                    var userClaims = User.FindAll(ClaimTypes.Role).ToList();
                    string mess = ($"User, named {user.userName} was successfully authentificated");
                    return RedirectToAction("Index", new { message = mess });

                }
                else
                {
                    ViewData["Message"] = "Аутентификация не прошла. Введите правильный логин и пароль или зарегестрируйтесь ";
                    return View(model);
                }
            }
            else 
            {
                ViewData["Message"] = "Аутентификация не прошла. Вы не ввели логин или пароль";
                return View(model);
            }
        }

        // GET: Users
        [HttpGet]
        [Authorize (Roles = "Admin")]
        //[Authorize(Roles ="Admin")]
        public async Task<IActionResult> Users()
        {
            var ListU = await _context.Users.ToListAsync();
            var UsersList = _userRepository.FindUsers();
            if (ListU == null)
            {
                ViewData["Message"] = "Entity set 'DatabaseContext.Users'  is null.";
                return View();
            }
            else
            {
                return View(ListU);
            }
        }
      

    }
}
