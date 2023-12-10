using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using TagsPractica.DAL.Models;
using TagsPractica.ViewModels;

namespace TagsPractica.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;
        private IMapper _mapper;

        // Метод-конструктор для инициализации
        public UserRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task AddUser(User user)
        {
            // Добавление пользователя
            user.Id = Guid.NewGuid();

            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);
               

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }

        public async Task ListRoles(User user)
        {
            // Добавление пользователя
            var listRoles = user.Roles.ToList();
        }

        public bool GetByLogin(string login)
        {
            //var user =_context.Users.FirstOrDefault(v => v.userName == login);
            bool validUser = _context.Users.Any(v => v.userName == login && v.password == login);
            return validUser;
        }

        public User GetByLogin2(string login)
        {
            //var user =_context.Users.FirstOrDefault(v => v.userName == login);
            //User user = new User();
            //user = _context.Users.Select(v => v.userName == login && v.password == login);
            
            var model = _context.Users.FirstOrDefaultAsync(m => m.userName == login);
            var rr = _mapper.Map<User>(model);
            
            //return _mapper.Map<User>(model);
            return rr;
        }

    }
}
