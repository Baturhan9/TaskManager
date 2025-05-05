using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    interface IUserRepository
    {
        IEnumerable<User> GetUsers(bool trackChanges);
        User GetUser(int userId, bool trackChanges);
        User GetUserByLoginAndPassword(string login, string password, bool trackChanges);
        void DeleteUser(User task);
        void CreateUser(User task);
    }
}