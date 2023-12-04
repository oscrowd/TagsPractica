using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TagsPractica.DAL;
using TagsPractica.DAL.Repositories;
using TagsPractica.Models;
using TagsPractica.ViewModels;
using System.Security.Authentication;

namespace TagsPractica.Controllers
{
    public class UsersController : Controller
    {
        private IMapper _mapper;
        private readonly DatabaseContext _context;
        private IUserRepository _userRepository;

        public UsersController(IMapper mapper, DatabaseContext context, IUserRepository userRepository)
        {
            _context = context;
            _userRepository = userRepository;
            _mapper = mapper;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            var user = await _context.Users.ToListAsync();
            //var entity =_mapper.Map<RegisterViewModel>(user);
            if (user == null)
            {
                return Problem("Entity set 'DatabaseContext.Users'  is null.");
            }
            else
            {
                return View();
            }
            
                          
                          
            
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
            return View();
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,userName,password,email")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
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
            var user = _mapper.Map<User>(model);
            //var roles = _db.UserProfiles.Include(c => c.UserGroup);.UserProfiles.Include(c => c.UserGroup);
            //var tempM = new RegisterViewModel();
            //var roles = _context.Roles.Select(p => new SelectListItem
            //{
            //    //Value = p.Id,
            //    Text = p.roleName
            //}).ToList();
            // return View(roles);
            //https://stackoverflow.com/questions/16814450/how-to-populate-a-textbox-based-on-dropdown-selection-in-mvc
            //return View(roles.ToList());
            //user.roleId = tempM.roleId;
            // Добавим в базу
            await _userRepository.AddUser(user);
            // Выведем результат
            Console.WriteLine($"User with id {user.Id}, named {user.userName} was successfully added on {user.email} and {user.roleId}");
            return View(model);
        }


        [HttpGet]
        public IActionResult  Authenticate()
        {
            return View();
        }

        [HttpPost]
        //[Route("authenticate")]
        public IActionResult Authenticate(RegisterViewModel model)
        {
            bool exist;
            if (String.IsNullOrEmpty(model.userName) || String.IsNullOrEmpty(model.password))
            {
                exist = false;
                return View(model);
            }
            //throw new ArgumentNullException("Запрос не корректен");
            //User user = _userRepository.GetByLogin(model);
            //exist = this.UserExistsName(model.userName);

            exist = _userRepository.GetByLogin(model.userName);

            model.existUser = exist;
            if (exist)
            {
                return RedirectToAction(nameof(HomeController.Index));
                //return RedirectToAction<HomeController>(m => m.LogIn());
            }
            else
            {
                return View(model);
            }



            //throw new AuthenticationException("Пользователь найден");
            //if (user.password != model.password)
            //    throw new AuthenticationException("Введенный пароль не корректен");
            //return _mapper.Map<RegisterViewModel>(user);

        }
    }
}
