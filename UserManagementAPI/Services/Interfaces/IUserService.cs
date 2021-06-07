using System.Collections.Generic;
using UserManagementAPI.Applications.Users;

namespace UserManagementAPI.Services.Interfaces
{
    public interface IUserService
    {
        List<UserResponse> GetUsers();
    }
}
