using TaskManager.Models.DataTransferObjects;

namespace TaskManager.Interfaces.Services
{
    public interface IUserAccessService
    {
        IEnumerable<UserAccessDTO> GetUserAccesses(bool trackChanges);
        UserAccessDTO GetUserAccess(int userAccessId, bool trackChanges);
        void CreateUserAccess(UserAccessDTO userAccess);
        void UpdateUserAccess(int userAccessId, UserAccessDTO userAccess);
        void DeleteUserAccess(int userAccessId);
    }
}
