using System.Collections.Generic;
using System.Threading.Tasks;
using API.Entities;
using API.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using API.DTOs;
using AutoMapper;
using AutoMapper.QueryableExtensions;


namespace API.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        public UserRepository(DataContext context, IMapper mapper)
        {

        }

        public async Task<MemberDto>GetMemberAsync(string username)
        {

            return await _context.Users
            .Where(x => x.UserName == username)
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .SingleOrDefaultAsync();
        }
        public Task<AppUser>GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
           // throw new System.NotImplementedException();
        }

         public Task<IEnumerable<MemberDto>>GetMemberAsync()
        {
            return await _context.Users
            .ProjectTo<MemberDto>(_mapper.ConfigurationProvider)
            .ToListAsync();
           // throw new System.NotImplementedException();
        }

        public Task<AppUser>GetUserByUsernameAsync(string username)
        {
             return await _context.Users
            .Include(p => p.Photos)
            .SingleOrDefaultAsync(x => x.UserName == username);
           // return await _context.Users.SingleOrdDefaultAsync(x => x.UserName == username);
            //throw new System.NotImplementedException();
        }
        
        public async Task<IAsyncEnumerable<AppUser>>GetUserAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public Task<bool>SaveAllAsync(){

            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(AppUser user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }
    }
}