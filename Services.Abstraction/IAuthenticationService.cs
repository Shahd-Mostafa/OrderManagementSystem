using Shared.AuthDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstraction
{
    public interface IAuthenticationService
    {
        Task<UserResultDto> RegisterAsync(RegisterDto registerDto);
        Task<UserResultDto> LoginAsync(LoginDto loginDto);
        //Task<UserResultDto> GetcurrentUserAsync(string email);
        Task<bool> isEmailExists(string email);
    }
}
