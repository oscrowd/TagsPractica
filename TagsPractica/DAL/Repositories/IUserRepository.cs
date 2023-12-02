using TagsPractica.Models;

namespace TagsPractica.DAL.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        //Task GetByLogin(model);
    }
}
