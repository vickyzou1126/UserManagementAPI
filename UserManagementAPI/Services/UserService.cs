using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using UserManagementAPI.Applications.Users;
using UserManagementAPI.DB.Repositories;
using UserManagementAPI.Services.Interfaces;

namespace UserManagementAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMapper _mapper;

        public UserService(IRepositoryFactory repoFactory, IMapper mapper)
        {
            this._repoFactory = repoFactory;
            this._mapper = mapper;
        }

        public List<UserResponse> GetUsers()
        {
            var users = _repoFactory.userRepository.GetAllUsers();
            return users.Select(user => _mapper.Map<UserResponse>(user)).ToList();
        }
    }
}
