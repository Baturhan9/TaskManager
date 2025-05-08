using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers(bool trackChanges);
        UserDTO GetUser(int userId, bool trackChanges);
        void CreateUser(UserDTO user);
        void UpdateUser(int userId, UserDTO user);
        void DeleteUser(int userId);
        UserDTO GetUserByEmailAndPassword(string email, string password, bool trackChanges);
    }
}
