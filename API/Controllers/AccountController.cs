using API.Data;
using API.Entities;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using System.Text;
using API.DTOs;
using Microsoft.EntityFrameworkCore;
using API.Interfaces;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _contex;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context, ITokenService tokenService)
        {
            _tokenService = tokenService;
            _contex = context;

        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>>Register(RegisterDto registerDto)
        {
            if(await UserExists(registerDto.Username))return BadRequest("User name is taken");

            using var hmac = new HMACSHA512();
            
            var user = new AppUser
            {
                UserName = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.password)),
                PasswordSalt = hmac.Key

            };

            _contex.Users.Add(user);
            await _contex.SaveChangesAsync();

            return new UserDto
            {
                UserName = user.UserName,
                Token = _tokenService.CreateToken(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<AppUser>>Login(LoginDto loginDto)
        {
            var user = await _contex.Users.
            SingleOrDefaultAsync(x => x.UserName == loginDto.UserName);

            if(user == null)return Unauthorized("Invald username");

            using var hmac = new HMACSHA512(user.PasswordSalt);

            var ComputeHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < ComputeHash.Length; i++)
            {

                if(ComputeHash[i] != user.PasswordHash[i])return Unauthorized("Invalid Passord");
            }

            return user;


        }

        private async Task<bool>UserExists(string username)
        {

            return await _contex.Users.AnyAsync(x => x.UserName == username.ToLower());

        }
    }
}