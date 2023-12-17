using System.Collections;
using System.Collections.Generic;
using TagsPractica.DAL.Models;
//using TagsPractica.ViewModels;

namespace TagsPractica.DAL.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(User user);
        bool GetByLogin(string userName);
        User GetByLogin2(string userName, string password);
        public Task<User> GetById(Guid id);
        public Task UpdateUser(User user, object model);
        public Task<IEnumerable<User>> FindUsers();

    }
}
