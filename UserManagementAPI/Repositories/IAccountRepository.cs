using System;
using System.Collections.Generic;
using System.Linq;
using UserManagementAPI.Data;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Repositories
{
    public interface IAccountRepository
    {
        Account CreateNewAccount(int id);
        List<Account> GetAllAccounts();
    }

    public class AccountRepository: IAccountRepository
    {
        private readonly ApplicationContext _context;
        private readonly IRepositoryFactory repositoryFactory;
        public AccountRepository(ApplicationContext context, IRepositoryFactory repositoryFactory)
        {
            this._context = context;
            this.repositoryFactory = repositoryFactory;
        }

        public Account CreateNewAccount(int id)
        {
            var account = new Account();
            try
            {
                var existingUser = repositoryFactory.userRepository.GetUserById(id);
                
                if (existingUser == null)//check if user not exist 
                    account.message.addStringIfNotExist($"user with id = {id} not found");
                else if (existingUser.Account != null)//has an account 
                    account.message.addStringIfNotExist($"user with id = {id} has an account");
                else if (!existingUser.hasCredit)//has no enough credit
                    account.message.addStringIfNotExist($"user with id = {id} has not enough credit");
                else
                {
                    account = new Account()
                    {
                        userId = id
                    };
                    _context.Accounts.Add(account);
                    _context.SaveChanges();
                    account.message.addStringIfNotExist($"new account for user {existingUser.name} has been created");
                }               
            }
            catch(Exception ex)
            {
                account.message.addStringIfNotExist(ex.Message);
            }
            return account;
        }

        public List<Account> GetAllAccounts()
        {
            return _context.Accounts.ToList();
        }
    }
}
