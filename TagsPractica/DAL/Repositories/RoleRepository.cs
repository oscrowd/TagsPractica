using Microsoft.EntityFrameworkCore;
using TagsPractica.Models;

namespace TagsPractica.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DatabaseContext _context;

        // Метод-конструктор для инициализации
        public RoleRepository(DatabaseContext context)
        {
            _context = context;
        }

        public async Task AddRole(Role role)
        {
            // Добавление роли
            //role.Id = Guid.NewGuid();

            // Добавление роли
            var entry = _context.Entry(role);
            if (entry.State == EntityState.Detached)
                await _context.Roles.AddAsync(role);

            // Сохранение изенений
            await _context.SaveChangesAsync();
        }
    }
}
