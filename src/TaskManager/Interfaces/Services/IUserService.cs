using TaskManager.Models;
using TaskManager.Models.CreateModelObjects;
using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers(bool trackChanges);
        UserDTO GetUser(int userId, bool trackChanges);
        void CreateUser(UserCreateDTO user);
        void UpdateUser(int userId, UserCreateDTO user);
        void DeleteUser(int userId);
        UserDTO GetUserByEmailAndPassword(string email, string password, bool trackChanges);
    }
}
