using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers(bool trackChanges);
        UserDTO GetUser(int userId, bool trackChanges);
        void CreateUser(UserForManipulationDTO user);
        void UpdateUser(int userId, UserForManipulationDTO user);
        void DeleteUser(int userId);
        UserDTO GetUserByEmailAndPassword(string email, string password, bool trackChanges);
    }
}
