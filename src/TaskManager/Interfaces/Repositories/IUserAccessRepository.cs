using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IUserAccessRepository
    {
        IEnumerable<UserAccess> GetUserAccesses(bool trackChanges);
        IEnumerable<UserAccess> GetUserAccessesByUserId(int userId, bool trackChanges);
        UserAccess GetUserAccessesByUserIdAndProjectId(
            int userId,
            int projectId,
            bool trackChanges
        );
        UserAccess GetUserAccess(int userAccessId, bool trackChanges);
        void DeleteUserAccess(UserAccess userAccess);
        void CreateUserAccess(UserAccess userAccess);
    }
}
