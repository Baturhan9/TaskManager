using AutoMapper;
using TaskManager.Exceptions.ModelsExceptions.NotFoundExceptions;
using TaskManager.Interfaces.Repositories;
using TaskManager.Interfaces.Services;
using TaskManager.Models.DataTransferObjects;
using TaskManager.Models.ManipulationDTO;

namespace TaskManager.Services
{
    public class UserAccessService : IUserAccessService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;

        public UserAccessService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public UserAccessDTO CreateUserAccess(UserAccessForManipulationDTO userAccess)
        {
            var userAccessDB = _mapper.Map<Models.UserAccess>(userAccess);
            _repositoryManager.UserAccess.CreateUserAccess(userAccessDB);
            _repositoryManager.Save();
            return _mapper.Map<UserAccessDTO>(userAccessDB);
        }

        public void DeleteUserAccess(int userAccessId)
        {
            var userAccess = _repositoryManager.UserAccess.GetUserAccess(
                userAccessId,
                trackChanges: false
            );
            if (userAccess is null)
                throw new NotFoundUserAccessException(userAccessId);

            _repositoryManager.UserAccess.DeleteUserAccess(userAccess);
            _repositoryManager.Save();
        }

        public UserAccessDTO GetUserAccess(int userAccessId, bool trackChanges)
        {
            var userAccess = _repositoryManager.UserAccess.GetUserAccess(
                userAccessId,
                trackChanges
            );
            if (userAccess is null)
                throw new NotFoundUserAccessException(userAccessId);

            return _mapper.Map<UserAccessDTO>(userAccess);
        }

        public IEnumerable<UserAccessDTO> GetUserAccesses(bool trackChanges)
        {
            var userAccesses = _repositoryManager.UserAccess.GetUserAccesses(trackChanges);
            return _mapper.Map<IEnumerable<UserAccessDTO>>(userAccesses);
        }

        public IEnumerable<UserAccessDTO> GetUserAccessesByUserId(int userId, bool trackChanges)
        {
            var user = _repositoryManager.User.GetUser(userId, trackChanges: false);
            if (user is null)
                throw new NotFoundUserException(userId);

            var userAccess = _repositoryManager.UserAccess.GetUserAccessesByUserId(userId, trackChanges);

            return _mapper.Map<IEnumerable<UserAccessDTO>>(userAccess);
        }

        public void UpdateUserAccess(int userAccessId, UserAccessForManipulationDTO userAccess)
        {
            var userAccessDB = _repositoryManager.UserAccess.GetUserAccess(
                userAccessId,
                trackChanges: true
            );
            if (userAccessDB is null)
                throw new NotFoundUserAccessException(userAccessId);

            _mapper.Map(userAccess, userAccessDB);
            _repositoryManager.Save();
        }
    }
}
