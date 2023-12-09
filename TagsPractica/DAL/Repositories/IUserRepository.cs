using TagsPractica.DAL.Models;

namespace TagsPractica.DAL.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        bool GetByLogin(string userName);
    }
}
