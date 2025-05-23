using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface IUserAccessService
    {
        IEnumerable<UserAccessDTO> GetUserAccesses(bool trackChanges);
        IEnumerable<UserAccessDTO> GetUserAccessesByUserId(int userId, bool trackChanges);
        UserAccessDTO GetUserAccess(int userAccessId, bool trackChanges);
        void CreateUserAccess(UserAccessForManipulationDTO userAccess);
        void UpdateUserAccess(int userAccessId, UserAccessForManipulationDTO userAccess);
        void DeleteUserAccess(int userAccessId);
    }
}
