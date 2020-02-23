using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Repositories;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepositoryFactory repoFactory;

        public UserController(IRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
        }

        [HttpGet]
        public List<User> Get()
        {
            return repoFactory.userRepository.GetAllUsers();
        }

        [HttpPost]
        [Route("CreatUser")]
        public User CreatUser([FromBody]User user)
        {
            user = user ?? new User();
            if (user.UserIsValid())
                repoFactory.userRepository.CreateNewUserAsync(user);
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
                var emailUser = repoFactory.userRepository.GetUserByEmailAddress(email);
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
            var user = repoFactory.userRepository.GetUserById(id);
            if (user == null)
            {
                user = new User();
                user.message.addStringIfNotExist($"user with id = {id} not found");
            }
            return user;
        }
    }
}