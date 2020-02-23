using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Repositories
{
    public interface IUserRepository
    {
        List<User> GetAllUsers();
        void CreateNewUserAsync(User user);
        User GetUserByEmailAddress(string email);
        User GetUserById(int id);
    }
    public class UserRepository: IUserRepository
    {
        private readonly ApplicationContext _context;
        public UserRepository(ApplicationContext context)
        {
            this._context = context;
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public void CreateNewUserAsync(User user)
        {
            try
            {
                //check unique email address
                var existingUser = GetUserByEmailAddress(user.email);
                if (existingUser == null)
                {
                    //create a new user
                    _context.Users.Add(user);
                    _context.SaveChanges();
                    user.message.addStringIfNotExist($"{user.name} : {user.email} has been created");
                }
                else
                    user.message.addStringIfNotExist($"{user.email} exists");
            }
            catch(Exception ex){
                user.message.addStringIfNotExist(ex.Message);
            }
        }

        public User GetUserByEmailAddress(string email)
        {
            return _context.Users.Where(x => x.email.Trim() == email.Trim())
                                 .Include(x=>x.Account).FirstOrDefault();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Where(x => x.id == id)
                                 .Include(x => x.Account).FirstOrDefault();
        }
    }
}
