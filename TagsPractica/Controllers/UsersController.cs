﻿using AutoMapper;
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
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Index()
        {
             await _context.Users.ToListAsync();
           
            //var entity =_mapper.Map<RegisterViewModel>(user);
            if (_context.Users == null)
            {
                return Problem("Entity set 'DatabaseContext.Users'  is null.");
            }
            else
            {
                return View(await _context.Users.ToListAsync());
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

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,userName,password,email")] RegisterViewModel model)
        {
            Guid guid;
            Guid.TryParse(model.Id, out guid);
            //User user = new User();
            var us = _userRepository.GetById(guid);


            if (id != guid)
            {
                return NotFound();
            }
            User user = new User();
            user = _mapper.Map<User>(model);

            //if (ModelState.IsValid)
            //{
            //var user = _mapper.Map<User>(model);
            try
            {
                //var temp=_context.Update(user);
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
            //}
            //return View(model);
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
            //RegisterViewModel model = new RegisterViewModel();  
            //if (id == null || _context.Users == null)
            //if (model != null)
            //{
            //    return RedirectToAction(nameof(Edit));
            //return View();
            //return NotFound();
            //}
            //Guid guid;
            //Guid.TryParse(model.Id, out guid);

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            RegisterViewModel model = new RegisterViewModel();
            model = _mapper.Map<RegisterViewModel>(user);
            return View(model);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPatch]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit2(Guid id, [Bind("Id,userName,password,email")] RegisterViewModel model)
        {
            Guid guid;
            Guid.TryParse(model.Id, out guid);
            //User user = new User();
            var user = _userRepository.GetById(guid);
            

            if (id != guid)
            {
                return NotFound();
            }


            //if (ModelState.IsValid)
            //{
                //var user = _mapper.Map<User>(model);
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
            //}
            //return View(model);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(RegisterViewModel model)
        {
            //if (id == null || _context.Users == null)
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


        [HttpGet]
        public IActionResult Authenticate2()
        {
            return View();
        }

        [HttpPost]
        //[Route("authenticate")]
        public async Task<IActionResult> Authenticate2(RegisterViewModel model)
        {

            User user =new User();
            if (String.IsNullOrEmpty(model.userName) || 
                String.IsNullOrEmpty(model.password))
                throw new ArgumentNullException("Запрос не корректен");

            user=_userRepository.GetByLogin2(model.userName, model.password);

            bool exist = _userRepository.GetByLogin(model.userName);

            if (exist)
            {
                string roleName = _roleRepository.GetById(user.roleId);
                user.Role.roleName = roleName;
                //var rr = _context.Users.Where(v => v.userName == model.userName && v.password == model.password);
                //user = rr.FirstOrDefault();
                
               
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
                //return RedirectToAction(nameof(Index));
                model = _mapper.Map<RegisterViewModel>(user);
                var userClaims = User.FindAll(ClaimTypes.Role).ToList();
                //return View(model);
                return RedirectToAction(nameof(Edit));

            }
            else 
            { 
                return View(model); 
            }
                        
            //user.existUser = exist;

            //return _mapper.Map<RegisterViewModel>(user);

            //throw new AuthenticationException("Пользователь найден");
            //if (user.password != model.password)
            //    throw new AuthenticationException("Введенный пароль не корректен");
            //return _mapper.Map<RegisterViewModel>(user);

        }
    }
}
