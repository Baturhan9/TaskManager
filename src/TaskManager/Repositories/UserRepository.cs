using TaskManager.Interfaces.Repositories;
using TaskManager.Models;

namespace TaskManager.Repositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(TaskManagerContext context)
            : base(context) { }

        public IEnumerable<User> GetUsers(bool trackChanges) =>
            FindAll(trackChanges).OrderBy(u => u.UserId).ToList();

        public User GetUser(int userId, bool trackChanges) =>
            FindByCondition(u => u.UserId.Equals(userId), trackChanges).SingleOrDefault();

        public User GetUserByLoginAndPassword(string login, string password, bool trackChanges) =>
            FindByCondition(u => u.Email.Equals(login) && u.Password.Equals(password), trackChanges)
                .SingleOrDefault();

        public void DeleteUser(User user) => Delete(user);

        public void CreateUser(User user) => Create(user);
    }
}
