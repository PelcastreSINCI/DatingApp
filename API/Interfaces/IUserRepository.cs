using API.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;
using API.DTOs;
using API.Interfaces;

namespace API.Interfaces
{
    public interface IUserRepository : IUserRepository
    {
         void Update(AppUser user);
         Task<bool>SaveAllAsync();

         Task<IEnumerable<AppUser>>GetUsersAsync();

        Task<AppUser> GetUserByIdAsync(int id);
        Task<AppUser>GetuserByysernameAsync(string username);
        Task<IEnumerable<MemberDto>>GetMembersAsync();

        Task<MemberDto>GetMemberAsyn(string username);

    }
}