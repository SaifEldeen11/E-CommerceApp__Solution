using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.DataTransferObjects.IdentityModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController(IServiceManger _serviceManger): ControllerBase
    {
        // LogIn
        [HttpPost("Login")] // POST : baseUrl/api/Authentication/Login
        public async Task<ActionResult<UserDto>> LogIn(LogInDto logInDto)
        {
            var user = await _serviceManger.AuthenticationService.LogInAsync(logInDto);
            return Ok(user);
        }

        // Register
        [HttpPost("Register")] // POST : baseUrl /api/Authentication   ion/Register
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            var user = await _serviceManger.AuthenticationService.RegisterAsync(registerDto);
            return Ok(user);
        }

        // Check Email 
        [HttpGet("CheckEmail")] // GET : baseUrl/api/Authentication/CheckEmail
        public async Task<ActionResult<bool>> CheckEmail(string email)
        {
            var result = await _serviceManger.AuthenticationService.CheckEmailAsync(email);
            return Ok(result);
        }

        // Get Current User
        [HttpGet("CurrentUser")]
        [Authorize]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var appUser = await _serviceManger.AuthenticationService.GetCurrentUserAsync(email);
            return Ok(appUser);
        }

        // Get Current User Address
        [HttpGet("Address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> GetCurrentUserAddress()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var address = await _serviceManger.AuthenticationService.GetCurrentUserAddressAsync(email);
            return Ok(address);
        }

        // Update Current User Address
        [HttpPut("Address")]
        [Authorize]
        public async Task<ActionResult<AddressDto>> UpdateCurrentUserAddress(AddressDto address)
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            var UpdatedAddress = await _serviceManger.AuthenticationService.UpdateCurrentUserAddressAsync(email!, address);
            return Ok(UpdatedAddress);
        }


    }
}
