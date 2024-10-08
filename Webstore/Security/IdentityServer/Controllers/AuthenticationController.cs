﻿ using AutoMapper;
using IdentityServer.Controllers.Base;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using IdentityServer.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers
{
    [Route("/api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : RegistrationControllerBase
    {
        private readonly IAuthenticationService _authService;
        public AuthenticationController(ILogger<AuthenticationController> logger, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IAuthenticationService authService) 
            : base(logger, mapper, userManager, roleManager)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterBuyer(NewUserDTO newUser)
        {
            return await RegisterNewUserWithRoles(newUser, new []{ "Buyer" });
        }

        [HttpPost("[action]")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterAdministrator(NewUserDTO newUser)
        {
            return await RegisterNewUserWithRoles(newUser, new string[] { "Administrator" });
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(AuthenticationModel),StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] UserCredentialsDTO userCredentials)
        {
            var user = await _authService.ValidateUser(userCredentials);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(await _authService.CreateAuthenticationModel(user));
        }


    }
}
// UserManager i RoleManager su iz IdentityServer-a, idealno je tu napraviti
// repozitorijum - interfejs koji sadrzi sve operacije koje su potrebne kontrolerima