using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class UserAccessRepository : RepositoryBase<UserAccess>, IUserAccessRepository
    {
        public UserAccessRepository(TaskManagerContext context)
            : base(context) { }

        public IEnumerable<UserAccess> GetUserAccesses(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(u => u.UserId).ToList();

        public IEnumerable<UserAccess> GetUserAccessesByUserId(int userId, bool trackChanges) =>
            FindByCondition(u => u.UserId.Equals(userId), trackChanges)
                .OrderBy(u => u.UserId)
                .ToList();

        public UserAccess GetUserAccess(int userAccessId, bool trackChanges) =>
            FindByCondition(u => u.UserAccessId.Equals(userAccessId), trackChanges)
                .SingleOrDefault();

        public void DeleteUserAccess(UserAccess userAccess) => Delete(userAccess);

        public void CreateUserAccess(UserAccess userAccess) => Create(userAccess);
    }
}
