using TagsPractica.Models;

namespace TagsPractica.DAL.Repositories
{
    public interface IBlogRepository
    {
        Task AddUser(User user);
    }
}
