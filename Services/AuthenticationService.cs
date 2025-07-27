using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Services.Abstraction;
using Shared;
using Shared.AuthDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class AuthenticationService: IAuthenticationService
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly JWTOptions _jwtOptions;
        public AuthenticationService(UserManager<User> userManager, IMapper mapper, IOptions<JWTOptions> options)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtOptions = options.Value;
        }
        public async Task<bool> isEmailExists(string email)
        {
            var existingEmail = await _userManager.Users.AnyAsync(x=>x.Email == email);
            return existingEmail;
        }

        public async Task<UserResultDto> LoginAsync(LoginDto loginDto)
        {
            var isValidEmail = new EmailAddressAttribute().IsValid(loginDto.Email);
            var user = isValidEmail ? await _userManager.FindByEmailAsync(loginDto.Email) : await _userManager.FindByNameAsync(loginDto.Email);
            if (user == null)
                throw new UnAuthorizedException();
            var isValidPassword = await _userManager.CheckPasswordAsync(user, loginDto.Password);
            if (!isValidPassword)
                throw new UnAuthorizedException();
            var roles = await _userManager.GetRolesAsync(user);
            return new UserResultDto
            (
                user.Id,
                user.UserName ?? " ",
                user.Email ?? " ",
                roles.FirstOrDefault() ?? " ",
                await CreateTokenAsync(user)
            );
        }

        public async Task<UserResultDto> RegisterAsync(RegisterDto registerDto)
        {
            var existingEmail = await _userManager.Users.AnyAsync(x=> x.Email==registerDto.Email);
            var existingUserName = await _userManager.Users.AnyAsync(u => u.UserName == registerDto.UserName);

            if (existingEmail)
                throw new BadRequestException("Email Already Exists.");

            if (existingUserName)
                throw new BadRequestException("Username Already Exists.");

            var user= new User { UserName = registerDto.UserName, Email= registerDto.Email};
            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
                throw new BadRequestException(string.Join("; ", result.Errors.Select(e => e.Description)));

            await _userManager.AddToRoleAsync(user, registerDto.Role);

            return new UserResultDto
            (
                user.Id,
                user.UserName ??" ",
                user.Email,
                registerDto.Role.Trim(),
                await CreateTokenAsync (user)
            );

        }

        private async Task<string> CreateTokenAsync(User user)
        {
            var authClaims = new List<Claim>
            {
                new Claim("userId",user.Id.ToString()),
                new Claim(ClaimTypes.Name,user.UserName ?? " "),
                new Claim(ClaimTypes.Email,user.Email?? " ")
            };
            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                expires: DateTime.Now.AddDays(_jwtOptions.DurationInDays),
                claims: authClaims,
                notBefore: DateTime.Now,
                signingCredentials: creds
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
