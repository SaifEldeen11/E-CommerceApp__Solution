using AutoMapper;
using Domain.Exceptions;
using Domain.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityModule;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceImplementation
{
    public class AuthenticationService(UserManager<ApplicationUser> _userManager,IConfiguration _configuration,IMapper _mapper) : IAuthenticationService
    {
        public async Task<bool> CheckEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return user is not null;
        }

        public async Task<AddressDto> GetCurrentUserAddressAsync(string email)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email ==email);

            if (user is null)
            {
                throw new UserNotFoundException(email);
            }

            if (user.Address is null)
            {
                throw new AddressNotFoundException(user.UserName!);
            }

            return _mapper.Map<Address,AddressDto>(user.Address);
        }

        public async Task<UserDto> GetCurrentUserAsync(string email)
        {
           var user = await _userManager.FindByEmailAsync(email);

            if(user is null)
            {
                throw new UserNotFoundException(email);
            }
            return new UserDto()
            {
                Email = user.Email!,
                DisplayName = user.DisplayName,
                Token = await CreateTokenAsync(user)
            };

        }
        public async Task<AddressDto> UpdateCurrentUserAddressAsync(string email, AddressDto addressDto)
        {
            var user = await _userManager.Users.Include(u => u.Address).FirstOrDefaultAsync(u => u.Email == email);

            if (user is null)
            {
                throw new UserNotFoundException(email);
            }

            if(user.Address is not null) // Update
            {
                user.Address.FirstName = addressDto.FirstName;
                user.Address.LastName = addressDto.LastName;
                user.Address.Street = addressDto.Street;
                user.Address.City = addressDto.City;
                user.Address.Country = addressDto.Country;
            }
            else // Add new address
            {
                user.Address = _mapper.Map<AddressDto, Address>(addressDto);
            }
            await _userManager.UpdateAsync(user);
            return _mapper.Map<Address, AddressDto>(user.Address);
        }

        public async Task<UserDto> LogInAsync(LogInDto logInDto)
        {
            // Check if Email exists
            var user = await _userManager.FindByEmailAsync(logInDto.Email);

            if (user == null)
            {
                throw new UserNotFoundException(logInDto.Email);
            }

            // Check if Password is correct

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, logInDto.Password);
            if (isPasswordValid)
            {
                return new UserDto
                {
                    Email = user.Email!,
                    DisplayName = user.UserName!,
                    Token = await CreateTokenAsync(user)
                };
            }

            throw new UnAuthorizedException();
        }

        public async Task<UserDto> RegisterAsync(RegisterDto registerDto)
        {
            // Mapping RegisterDto to ApplicationUser
            var user = new ApplicationUser
            {
                Email = registerDto.Email,
                UserName = registerDto.UserName,
                DisplayName = registerDto.DisplayName,
                PhoneNumber = registerDto.PhoneNumber
            };

            var Result = await _userManager.CreateAsync(user, registerDto.Password);

            if (Result.Succeeded)
            {
                return new UserDto
                {
                    Email = user.Email!,
                    DisplayName = user.UserName!,
                    Token = await CreateTokenAsync(user)
                };
            }
            else
            {
                throw new BadRequestException(Result.Errors.Select(e => e.Description).ToList());
            }

        }


        private async Task<string> CreateTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Email, user.Email!),
                new Claim(ClaimTypes.Name, user.UserName!),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var MySecretKey = _configuration.GetSection("JWTOptions")["SecretKey"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MySecretKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
            issuer: _configuration["JWTOptions:Issuer"],
            audience: _configuration["JWTOptions:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
            );


            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
