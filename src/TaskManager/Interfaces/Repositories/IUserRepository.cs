using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers(bool trackChanges);
        User GetUser(int userId, bool trackChanges);
        User GetUserByLoginAndPassword(string login, string password, bool trackChanges);
        void DeleteUser(User user);
        void CreateUser(User user);
    }
}
