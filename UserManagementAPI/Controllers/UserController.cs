using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.DB.Models;
using UserManagementAPI.DB.Repositories;
using UserManagementAPI.Extensions;
using MediatR;
using AutoMapper;
using UserManagementAPI.Services.Interfaces;
using UserManagementAPI.Applications.Users;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryFactory _repoFactory;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IRepositoryFactory repoFactory, IMediator mediator, IMapper mapper, IUserService userService)
        {
            this._repoFactory = repoFactory;
            this._mediator = mediator;
            this._mapper = mapper;
            this._userService = userService;
        }


        [HttpGet]
        [Route("/users")]
        public List<UserResponse> Get()
        {
            return _userService.GetUsers();
        }

        [HttpPost]
        [Route("CreatUser")]
        public User CreatUser([FromBody]User user)
        {
            user = user ?? new User();
            if (user.UserIsValid())
                _repoFactory.userRepository.CreateNewUserAsync(user);
            return user;
        }

        [HttpGet]
        [Route("GetUserByEmail/{email}")]
        public User GetUserByEmail(string email)
        {
            var user = new User();
            if (email == null || string.IsNullOrEmpty(email.Trim()))
                user.message.addStringIfNotExist("Please provide an email address");
            else
            {
                var emailUser = _repoFactory.userRepository.GetUserByEmailAddress(email);
                if (emailUser == null)
                    user.message.addStringIfNotExist($"user with email {email.Trim()} not found");
                else
                    return emailUser;
            }
            return user;
        }

        [HttpGet]
        [Route("GetUserById/{id}")]
        public User GetUserById(int id)
        {
            var user = _repoFactory.userRepository.GetUserById(id);
            if (user == null)
            {
                user = new User();
                user.message.addStringIfNotExist($"user with id = {id} not found");
            }
            return user;
        }
    }
}