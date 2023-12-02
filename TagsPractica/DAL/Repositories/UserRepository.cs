using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using TagsPractica.Models;

namespace TagsPractica.DAL.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseContext _context;

        // Метод-конструктор для инициализации
        public UserRepository(DatabaseContext context)
        {
            _context = context;
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

        public async Task GetByLogin(string userName, string password)
        {
            //_context.Users.Any(userName == userName).GetValueOrDefault();
            
            // bool exist = _context.ContextId(user.Id);
            //_context.Users.Contains(model);
        }
    }
}
