using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TagsPractica.DAL;
using TagsPractica.API.ViewModels;
using System.Security.Authentication;
using TagsPractica.DAL.Repositories;
using TagsPractica.DAL.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Ajax.Utilities;

namespace TagsPractica.API.Controllers
{
    [ApiController]
    [Route("controller")]
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
        public async Task<IActionResult> Index()
        {
            var ListU = await _context.Users.ToListAsync();
            var UsersList = _userRepository.FindUsers();
            if (ListU == null)
            {
                return StatusCode(200, "Пользователей нет в БД");
            }
            else
            {
                return StatusCode(200, UsersList);
            }
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

                bool exist = _userRepository.GetByLogin(model.userName, model.password);

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
                    //string mess = ($"User, named {user.userName} was successfully authentificated");
                    //return RedirectToAction("Index", new { message = mess });
                    return StatusCode(200, "Пользователь " + user.userName + " аутентифицирован");

                }
                else
                {
                    return StatusCode(200, "Пользователь " + user.userName + " не аутентифицирован");

                }
            }
            else
            {
                return StatusCode(200, "Введенные данные не валидны");
            }
        }

        [HttpPatch]
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
                return StatusCode(200, mess);
            }
            else
            {
                return StatusCode(400, model);
            }
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.Users == null)
            {
                return StatusCode(200, "Нет пользователей в БД");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return StatusCode(200, "Пользователь  удален");
        }

    }
    
}
