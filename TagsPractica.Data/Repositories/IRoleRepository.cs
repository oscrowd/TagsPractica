using TagsPractica.DAL.Models;

namespace TagsPractica.DAL.Repositories
{
    public interface IRoleRepository
    {
        Task AddRole(Role role);
        string GetById(int id);
    }
}
