using UserManagementAPI.Controllers;
using UserManagementAPI.Models;
using Xunit;

namespace UserManagementTest
{
    public class AccountControllerTest : BaseTest<AccountController>
    {
        [Fact]
        public void Get_test()
        {
            var accounts = _controller.Get();
            Assert.Single(accounts);
        }

        #region CreateAccountByUserId
        [Fact]
        public void CreateAccountByUserId_nullUser()
        {
            var account = _controller.CreateAccountByUserId(null);
            Assert.Equal(0,account.id);
        }
        [Fact]
        public void CreateAccountByUserId_Valid()
        {
            var account = _controller.CreateAccountByUserId(new User() {id = 1});
            Assert.Equal(1,account.message.Count);
        }
        [Fact]
        public void CreateAccountByUserId_hasNoUser()
        {
            var account = _controller.CreateAccountByUserId(new User() { id = 0 });
            Assert.Equal(0, account.id);
        }
        [Fact]
        public void CreateAccountByUserId_hasNoEnoughCredit()
        {
            var account = _controller.CreateAccountByUserId(new User() { id = 2 });
            Assert.Contains("user with id = 2 has not enough credit", account.message);
        }
        [Fact]
        public void CreateAccountByUserId_hasAccount()
        {
            var account = _controller.CreateAccountByUserId(new User() { id = 5 });
            Assert.Equal(0, account.id);
            Assert.Contains("user with id = 5 has an account", account.message);
        }
        #endregion
    }
}
