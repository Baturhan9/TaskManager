using TaskManager.Models;

namespace TaskManager.Interfaces.Repositories
{
    public interface IUserAccessRepository
    {
        IEnumerable<UserAccess> GetUserAccesses(bool trackChanges);
        IEnumerable<UserAccess> GetUserAccessesByUserId(int userId, bool trackChanges);
        UserAccess GetUserAccess(int userAccessId, bool trackChanges);
        void DeleteUserAccess(UserAccess task);
        void CreateUserAccess(UserAccess task);
    }
}