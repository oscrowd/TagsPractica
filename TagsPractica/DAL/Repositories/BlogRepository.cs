using Microsoft.EntityFrameworkCore;
using TagsPractica.Models;

namespace TagsPractica.DAL.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly DatabaseContext _context;

        // Метод-конструктор для инициализации
        public BlogRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddUser(User user)
        {
            // Добавление пользователя
            var entry = _context.Entry(user);
            if (entry.State == EntityState.Detached)
                await _context.Users.AddAsync(user);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }
    }
}
