using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserManagementAPI.Data;
using UserManagementAPI.Models;
using UserManagementAPI.Repositories;

namespace UserManagementTest
{
    public class BaseTest<T>  where T: ControllerBase
    {
        protected ApplicationContext _context;
        protected IRepositoryFactory _repositoryFactory;
        protected T _controller;
        public BaseTest()
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            optionsBuilder.UseInMemoryDatabase(typeof(T).ToString()+"InMemoryDb");
            _context = new ApplicationContext(optionsBuilder.Options);
            //insert fake data
            InsertFakeData();
            _repositoryFactory = new RepositoryFactory(_context);
            _controller = Create();
        }
        #region Private Methods
        private T Create()
        {
            var constructors = typeof(T).GetConstructors();
            var expectedPara = new object[] { _repositoryFactory };
            foreach(var cm in constructors)
            {
                var args = cm.GetParameters();
                var paras = new List<object>();
                if (args.Length > expectedPara.Length)
                    continue;
                for(int i = 0; i < args.Length; i++)
                {
                    if (args[i].ParameterType.IsInstanceOfType(expectedPara[i]))
                    {
                        paras.Add(expectedPara[i]);
                    }
                }
                return (T)Activator.CreateInstance(typeof(T), paras.ToArray());
            }
            throw new ArgumentNullException();
        }
        private void Clear()
        {
            _context.Users.RemoveRange(_context.Users.ToArray());
            _context.Accounts.RemoveRange(_context.Accounts.ToArray());
            _context.SaveChanges();
        }
        private void InsertFakeData()
        {
            Clear();
            _context.Users.Add(new User() { id = 1,name = "name1", email = "test1@test.com", salary = 10000, expense = 5000 });
            _context.Users.Add(new User() { id = 2,name = "name2", email = "test2@test.com", salary = 5800, expense = 5000 });
            _context.Users.Add(new User() { id = 3, name = "name3", email = "test3@test.com", salary = 68000, expense = 4200 });
            _context.Users.Add(new User() { id = 4, name = "name4", email = "test4@test.com", salary = 7800, expense = 2000 });
            _context.Users.Add(new User() { id = 5,name = "name5", email = "test5@test.com", salary = 5000, expense = 4000, Account = new Account()
            {
                id = 5,
                userId = 5
            }
            });
            _context.SaveChanges();
        }
        #endregion
    }
}
