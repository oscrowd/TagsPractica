using Microsoft.EntityFrameworkCore;
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
    }
}
