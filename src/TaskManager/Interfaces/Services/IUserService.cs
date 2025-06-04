using TaskManager.Models;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;
using TaskManager.Models.Shared;

namespace TaskManager.Interfaces.Services
{
    public interface IUserService
    {
        IEnumerable<UserDTO> GetUsers(bool trackChanges);
        Dictionary<int, UserListDto> GetUsersDto(bool trackChanges);
        UserDTO GetUser(int userId, bool trackChanges);
        UserDTO CreateUser(UserForManipulationDTO user);
        void UpdateUser(int userId, UserForManipulationDTO user);
        void DeleteUser(int userId);
        UserDTO GetUserByEmail(string email, bool trackChanges);
    }
}
