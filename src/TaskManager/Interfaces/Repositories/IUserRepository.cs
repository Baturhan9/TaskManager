using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers(bool trackChanges);
        User GetUser(int userId, bool trackChanges);
        User GetUserByEmail(string login, bool trackChanges);
        void DeleteUser(User user);
        void CreateUser(User user);
    }
}
