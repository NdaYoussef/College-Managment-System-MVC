using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Application.DTOs;
using UniManagementSystem.Application.DTOs.UserDtos;
using UniManagementSystem.Application.Interfaces;
using UniManagementSystem.Domain.Models;

namespace UniManagementSystem.Application.Services
{
    public class AccountServicecs : IAccountServicecs
    {
       // private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountServicecs(UserManager<ApplicationUser> userManager, 
            SignInManager<ApplicationUser> signInManager, ITokenService tokenService, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
           // _httpContextAccessor = httpContextAccessor;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> GetCurrentUserAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
                return null;
            var currentUser = await _userManager.FindByEmailAsync(email);
            if(currentUser is not null)
            return currentUser;

            return null;
        }

        public async Task<AuthDto> RegisterAsync(RegisterDto registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) is not null)
                return new AuthDto { Message = "Email is already Registered!" };
            //if (await _userManager.FindByNameAsync(registerDto.UserName) is not null)
            //    return new AuthDto { Message = "UserName is already Registered!" };

            var user = new ApplicationUser
            {
                //UserName = registerDto.UserName,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                Email = registerDto.Email,
                NationalID = registerDto.NationalID,
            //    Address = registerDto.Address,
            //    PhoneNumber = registerDto.PhoneNumber,
                Gender = registerDto.Gender,
            //    DateOfBirth = registerDto.DateOfBirth,
            };
            await _userManager.SetUserNameAsync(user,user.UserName);
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                    errors += $"{error.Description}";

                return new AuthDto { Message = errors };
            }
            var roleName = registerDto.Role.ToString();

            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var roleResult = await _roleManager.CreateAsync(new IdentityRole(roleName));
                if (!roleResult.Succeeded)
                {
                   // await _userManager.DeleteAsync(user);
                   user.IsDeleted = true;
                    return new AuthDto
                    {
                        Message = "Something gets wrong, try, again later,",
                        IsAuthenticated = false
                    };
                }
            }
            var newResult = await _userManager.AddToRoleAsync(user, roleName);
            if (!newResult.Succeeded)
            {
                //var error = string.Join(" ", newResult.Errors.Select(e => e.Description));
                //return new AuthDto
                //{
                //    IsAuthenticated = false,
                //    Message = $"Failed to assign role: {error}",
                //};
               var error =  new IdentityError
                {
                    Description = $"{newResult.Errors}"
                };
                return new AuthDto
                {

                    IsAuthenticated = false,
                    Message = $"Failed to assign role: {error}",
                };
            }

            await _signInManager.SignInAsync(user, isPersistent: false);
            var assignedRoles = (await _userManager.GetRolesAsync(user)).ToList();
            return new AuthDto
            {
                Email = user.Email,
                UserName = user.UserName,
                Roles = assignedRoles,
                IsAuthenticated = true,
                Message = "User registered successfully"
            };
        }
        public async Task<AuthDto> LoginAsync(LoginDto loginDto)
        {
            var authModel = new AuthDto();
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                return new AuthDto() { Message = "Email or Password is incorrect!" };


            var roles = (await _userManager.GetRolesAsync(user)).ToList();

            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, false, false);
            if (result.Succeeded)
            {
                authModel.IsAuthenticated = true;
                authModel.Email = user.Email;
                authModel.UserName = user.UserName;
                authModel.Roles = roles; 
            }

            if(user.RefreshTokens!= null && user.RefreshTokens.Any(t=>t.IsActive))
            {
                var refreshToken = user.RefreshTokens.First(t => t.IsActive);
                authModel.RefreshToken = refreshToken.Token;
                authModel.RefreshTokenExpiration = refreshToken.ExpiresOn;
            }
            else
            {
                var newRefreshToken = _tokenService.GenerateRefreshToken();
                authModel.RefreshToken = newRefreshToken.Token;
                authModel.RefreshTokenExpiration = newRefreshToken.ExpiresOn;
                user.RefreshTokens?.Add(newRefreshToken);
                await _userManager.UpdateAsync(user);
            }
            return authModel;
        }

        public async Task<AuthDto> LogoutAsync(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "Email is required to logout."
                };
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "User not found."
                };
            }
            if (user.RefreshTokens != null)
            {
                var activeTokens = user.RefreshTokens.Where(t => t.IsActive).ToList();
                foreach (var token in activeTokens)
                {
                    token.ExpiresOn = DateTime.UtcNow;
                }

                await _userManager.UpdateAsync(user);
            }
           
            await _signInManager.SignOutAsync();
            return new AuthDto
            {
                IsAuthenticated = true,
                Message = "User has been logged out sucessfully"
            };
        }


        }
        }
    

