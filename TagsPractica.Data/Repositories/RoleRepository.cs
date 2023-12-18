using Microsoft.EntityFrameworkCore;
using TagsPractica.DAL.Models;

namespace TagsPractica.DAL.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly DatabaseContext _context;
        private readonly List<Role> _roles = new List<Role>();
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

        public IEnumerable<Role> GetAll()
        {
            return _roles;
        }

        public string GetById(int id)
        {
            //var user =_context.Users.FirstOrDefault(v => v.userName == login);
            //User user = new User();
            Role role = new Role();
            var r = _context.Roles.Where(v => v.Id == id);
            role = r.FirstOrDefault();
            if (role == null)
            {
                return "";
            }
            else
            {
                return role.roleName;
            }
            //var rr = _mapper.Map<User>(model);




        }

    }
}
