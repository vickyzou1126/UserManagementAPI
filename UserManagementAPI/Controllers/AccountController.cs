using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Models;
using UserManagementAPI.Repositories;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IRepositoryFactory repoFactory;

        public AccountController(IRepositoryFactory repoFactory)
        {
            this.repoFactory = repoFactory;
        }

        [HttpGet]
        public List<Account> Get()
        {
            return repoFactory.accountRepository.GetAllAccounts();
        }

        [HttpPost]
        [Route("CreateAccountByUserId")]
        public Account CreateAccountByUserId([FromBody]User user)
        {
            Account account = new Account();
            if (user == null)
                account.message.addStringIfNotExist("please provide a user id");
            else
                account = repoFactory.accountRepository.CreateNewAccount(user.id);
            return account;
        }
    }
}