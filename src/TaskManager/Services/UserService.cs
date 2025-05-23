using AutoMapper;
using TaskManager.Exceptions;
using TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UserService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public UserDTO CreateUser(UserForManipulationDTO user)
        {
            var userDB = _mapper.Map<Models.User>(user);
            _repositoryManager.User.CreateUser(userDB);
            _repositoryManager.Save();
            return _mapper.Map<UserDTO>(userDB);
        }

        public void DeleteUser(int userId)
        {
            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user == null)
                throw new NotFoundUserException(userId);

            _repositoryManager.User.DeleteUser(user);
            _repositoryManager.Save();
        }

        public UserDTO GetUser(int userId, bool trackChanges)
        {
            var user = _repositoryManager.User.GetUser(userId, trackChanges);
            if (user == null)
                throw new NotFoundUserException(userId);

            return _mapper.Map<UserDTO>(user);
        }

        public UserDTO GetUserByEmailAndPassword(string email, string password, bool trackChanges)
        {
            var user = _repositoryManager.User.GetUserByLoginAndPassword(
                email,
                password,
                trackChanges
            );
            if (user == null)
                throw new NotFoundException("Not found user with this email and password");

            return _mapper.Map<UserDTO>(user);
        }

        public IEnumerable<UserDTO> GetUsers(bool trackChanges)
        {
            var users = _repositoryManager.User.GetUsers(trackChanges);
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }

        public void UpdateUser(int userId, UserForManipulationDTO user)
        {
            var userDB = _repositoryManager.User.GetUser(userId, trackChanges: true);
            if (userDB == null)
                throw new NotFoundUserException(userId);

            _mapper.Map(user, userDB);
            _repositoryManager.Save();
        }
    }
}
