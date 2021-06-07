using AutoMapper;
using UserManagementAPI.Applications.Users;
using UserManagementAPI.DB.Models;

namespace UserManagementAPI.Map
{
    public class UserMapper : Profile
    {
        public UserMapper()
        {
            CreateMap<User, UserResponse>()
                .ForMember(d => d.hasCredit, o => o.MapFrom(r => (r.salary - r.expense) >= 1000));
        }
    }  
}
