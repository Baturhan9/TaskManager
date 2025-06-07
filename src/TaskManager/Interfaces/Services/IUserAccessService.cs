using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Interfaces.Services
{
    public interface IUserAccessService
    {
        IEnumerable<UserAccessDTO> GetUserAccesses(bool trackChanges);
        IEnumerable<UserAccessDTO> GetUserAccessesByUserId(int userId, bool trackChanges);
        IEnumerable<UserAccessDTO> GetUserAccessesByProjectId(int projectId, bool trackChanges);
        UserAccessDTO GetUserAccess(int userAccessId, bool trackChanges);
        UserAccessDTO CreateUserAccess(UserAccessForManipulationDTO userAccess);
        void UpdateUserAccess(int userAccessId, UserAccessForManipulationDTO userAccess);
        void DeleteUserAccess(int userAccessId);
    }
}
