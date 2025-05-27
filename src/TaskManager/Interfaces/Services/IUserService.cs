using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers(bool trackChanges);
        UserDTO GetUser(int userId, bool trackChanges);
        UserDTO CreateUser(UserForManipulationDTO user);
        void UpdateUser(int userId, UserForManipulationDTO user);
        void DeleteUser(int userId);
        UserDTO GetUserByEmail(string email, bool trackChanges);
    }
}
