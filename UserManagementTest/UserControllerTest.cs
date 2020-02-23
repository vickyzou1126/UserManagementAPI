using Xunit;
using UserManagementAPI.Controllers;
using User = UserManagementAPI.Models.User;
namespace UserManagementTest
{
    public class UserControllerTest : BaseTest<UserController>
    { 
        [Fact]
        public void Get_test()
        {
            var users = _controller.Get();
            Assert.Equal(5, users.Count);
        }

        #region CreateNewUser
        [Fact]
        public void CreateNewUser_test_NullUser()
        {
            var user = _controller.CreatUser(null);
            Assert.Equal(0,user.id);
        }

        [Fact]
        public void CreateNewUser_test_missingValue()
        {
            var user = _controller.CreatUser(new User()
            {
                name = "test",
                salary = 10000,
                expense = 5000
            });
            Assert.Equal(0,user.id);
        }

        [Fact]
        public void CreateNewUser_test_withExistingEmail()
        {
            var user = _controller.CreatUser(new User()
            {
                name = "test1",
                email = "test1@test.com",
                salary = 10000,
                expense = 5000
            });
            Assert.Equal(0,user.id);
        }

        [Fact]
        public void CreateNewUser_ValidUser()
        {
            var user = _controller.CreatUser(new User()
            {
                name = "test1",
                email = "noTest@test.com",
                salary = 2000,
                expense = 1500
            });
            Assert.NotEqual(0,user.id);
        }
        #endregion

        #region GetUserByEmail
        [Fact]
        public void GetUserByEmail_nullEmail()
        {
            var user = _controller.GetUserByEmail(null);
            Assert.Contains("Please provide an email address", user.message);
        }

        [Fact]
        public void GetUserByEmail_invalidEmail()
        {
            var user = _controller.GetUserByEmail("a@a.com");
            Assert.Contains($"user with email a@a.com not found", user.message);
        }

        [Fact]
        public void GetUserByEmail_validEmail()
        {
            var user = _controller.GetUserByEmail("test1@test.com");
            Assert.Empty(user.message);
        }
        #endregion

        #region GetUserById
        [Fact]
        public void GetUserById_invalidId()
        {    
            var user = _controller.GetUserById(-1);
            Assert.Contains("user with id = -1 not found",user.message);
        }

        [Fact]
        public void GetUserById_validId()
        {
            var user = _controller.GetUserById(1);
            Assert.Empty(user.message);
        }
        #endregion
    }
}
