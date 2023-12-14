using System.Collections;
using TagsPractica.DAL.Models;

namespace TagsPractica.DAL.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        bool GetByLogin(string userName);
        User GetByLogin2(string userName, string password);
        Task GetById(Guid id);
    }
}
