using IdentityServiceApi.Context;
using IdentityServiceApi.Contrat;
using IdentityServiceApi.Dto;
using IdentityServiceApi.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServiceApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext context;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IJwtTokenHelper jwt;

        public AuthService(AppDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IJwtTokenHelper jwt)
        {
            this.context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.jwt = jwt;
        }
        public async Task<LoginResponseDto> LoginAsync(LoginDto user)
        {
            var userExist = await context.Users.FirstOrDefaultAsync(u => u.UserName.Equals(user.UserName));

            if (userExist is not null)
            {
                var isPasswordValid = await userManager.CheckPasswordAsync(userExist, user.Password);

                if (!isPasswordValid)
                {
                    return new LoginResponseDto() { Token = "", User = null };
                }
                // generate token

                return new LoginResponseDto
                {
                    Token = jwt.GeneratToken(userExist),
                    User = new UserDto
                    {
                        Email = userExist.Email,
                        ID = userExist.Id,
                        Name = userExist.Name,
                        PhoneNumber = userExist.PhoneNumber
                    }
                };
            }
            return new LoginResponseDto() { Token = "", User = null };

        }

        public async Task<ResponseDto> RegisterAsync(RegistrationDto newUser)
        {
            ApplicationUser user = new()
            {
                UserName = newUser.Email,
                Email = newUser.Email,
                PhoneNumber = newUser.PhoneNumber,
                Name = newUser.Name,
            };

            try
            {
                var created = await userManager.CreateAsync(user, newUser.Password);

                if (created.Succeeded)
                {
                    var test = await context.ApplicationUsers.FindAsync(user.Id);
                    var userCreated = await context.ApplicationUsers.FirstAsync(u => u.UserName == user.UserName);

                    UserDto result = new()
                    {
                        Email = userCreated.Email,
                        ID = userCreated.Id,
                        Name = userCreated.Name,
                        PhoneNumber = userCreated.PhoneNumber,
                    };

                    return new ResponseDto
                    {
                        IsSuccess = true,
                        Result = result,
                    };
                }

                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = string.Join(", ", created.Errors.Select(e => e.Description))
                };

            }
            catch (Exception ex)
            {

                return new ResponseDto
                {
                    IsSuccess = false,
                    Message = ex.Message
                };

            }
        }
    }
}
