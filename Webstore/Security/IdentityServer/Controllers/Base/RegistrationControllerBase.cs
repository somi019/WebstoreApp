﻿using AutoMapper;
using IdentityServer.DTOs;
using IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace IdentityServer.Controllers.Base
{
    public class RegistrationControllerBase : ControllerBase
    {
        protected readonly ILogger<AuthenticationController> _logger;
        protected readonly IMapper _mapper;
        protected readonly UserManager<User> _userManager;
        protected readonly RoleManager<IdentityRole> _roleManager;

        public RegistrationControllerBase(ILogger<AuthenticationController> logger, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        protected async Task<IActionResult> RegisterNewUserWithRoles(NewUserDTO newUserDTO, IEnumerable<string> roles)
        {
            var user = _mapper.Map<User>(newUserDTO);
            var result = await _userManager.CreateAsync(user, newUserDTO.Password);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.TryAddModelError(error.Code, error.Description);

                }
                return BadRequest(ModelState);
            }

            _logger.LogInformation($"Successfully registered user: {user.UserName}");

            foreach (var role in roles)
            {
                var roleExists = await _roleManager.RoleExistsAsync(role);

                if(roleExists)
                {
                    await _userManager.AddToRoleAsync(user, role);
                    _logger.LogInformation($"Added a role {role} to a user: {user.UserName}");
                }
                else
                {
                    _logger.LogInformation($"Role {role} does not exist.");

                }
            }

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
