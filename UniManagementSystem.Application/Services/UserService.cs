using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Application.DTOs;
using UniManagementSystem.Application.DTOs.UserDtos;
using UniManagementSystem.Application.Interfaces;
using UniManagementSystem.Domain.Enums;
using UniManagementSystem.Domain.Models;
using UniManagementSystem.Infrastructure.DBContext;

namespace UniManagementSystem.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IMapper _mapper;
        private readonly UniSystemContext _context;
        private readonly ICloudinaryService _cloudinaryService;
        private readonly RoleManager<ApplicationUser> _roleManager;

        #region Ctor
        public UserService(UserManager<ApplicationUser> userManager, IMapper mapper,
           UniSystemContext context, ICloudinaryService cloudinaryService, RoleManager<ApplicationUser> roleManager)
        {
            _userManager = userManager;
            _mapper = mapper;
            _context = context;
            _cloudinaryService = cloudinaryService;
            _roleManager = roleManager;
        } 
        #endregion


        public async Task<AuthDto> GetUserData(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            
            if(user is null)
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = " User not found"
                };
            }

            var roles = await _userManager.GetRolesAsync(user);

            //string dashboardRoute = string.Empty;
            //if (roles.Contains("Admin")) dashboardRoute = "/Admin/Dashboard";
            //else if (roles.Contains("Lecturer")) dashboardRoute = "/Lecturer/Dashboard";
            //else if (roles.Contains("Student")) dashboardRoute = "/Student/Dashboard";
            //else dashboardRoute = "/Dashboard";


            string dashboardRoute = roles.FirstOrDefault()! switch
            {
                "Admin"=> "/Admin/Dashboard",
                "Lecturer" => "/Lecturer/Dashboard",
                "Student" => "/Student/Dashboard",
            };



            var dto = _mapper.Map<UserDashboardDto>(user);
            var data = new
            {
                user = dto,
                Roles = roles,
                DashboardRoute = dashboardRoute
            };
            return new AuthDto
            {
                IsAuthenticated = true,
                Message = "User data retrieved",
                Data = data
            };
        }
        public  async Task<(bool IsSuccess, string message)> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user is null) 
                return (false,"User not found");

            var result = await _userManager.ChangePasswordAsync(user, oldPassword, newPassword);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
            }

          return (true,"Password changed successfully");

            }

        public async Task<AuthDto> ChangeProfilePictureAsync(string userId,  IFormFile ProfileImage)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser is null)
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "User not found"
                };
            var imageUrl = await _cloudinaryService.UploadImageAsync(ProfileImage);
            currentUser.ProfilePic = imageUrl;
            if (string.IsNullOrEmpty(imageUrl))
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "Image upload failed"
                };
            }

            var result = await _userManager.UpdateAsync(currentUser);
            if (!result.Succeeded)
            {
                var errors = string.Empty ;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = errors
                };
            }

            var dto = _mapper.Map<UserDashboardDto>(currentUser);
            return new AuthDto
            {
                IsAuthenticated = true,
                Message = "Profile picture updated successfully",
                Data = dto
            };
        }

        public async Task<AuthDto> GetAllUsers()
        {
            //var users = await _context.Users.AsNoTracking().ToListAsync();
            //var result = new List<UserInfoDto>();

            //foreach(var user in users)
            //{
            //    var role = await _userManager.GetRolesAsync(user);
            //    result.Add(new UserInfoDto
            //    {
            //        UserId = user.Id,
            //        UserName = user.UserName,
            //        Role = role.ToString(),
            //    });
            //}
            ////////////////##Code Refactoring 

            var users = await _context.Users.AsNoTracking().ToListAsync();


            var tasks = users.Select(async user => new UserInfoDto
            {
                UserId = user.Id,
                UserName = user.UserName,
                Role = string.Join(",",await _userManager.GetRolesAsync(user))
            });

           var result = await Task.WhenAll(tasks);
            return new AuthDto
            {
                IsAuthenticated = true,
                Data = result,
                Message = "Users retrived successfully!",
            };
        }

        public async Task<AuthDto> CreateUserAsync(CreatUserDto dto)
        {
            if (dto is null)
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "Invalid request data",
                };
            }
            var currebtUser = await _userManager.FindByEmailAsync(dto.Email);
            if (currebtUser is not null)
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "User already found",
                };
            }

           var newUser =  dto.Adapt<ApplicationUser>();
            var result = await _userManager.CreateAsync(newUser, dto.Password);
            if(!result.Succeeded)
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = string.Join(", ", result.Errors.Select(e => e.Description))
                };
            }
            return new AuthDto
            {
                IsAuthenticated = true,
                Message = "User created successfully!",
                Data = newUser.Email
            };
        }

        public async Task<AuthDto> EditUserAsync(EditUserDto dto)
        {
            if(dto is null)
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "Invalid request data",
                };
            }
           var currebtUser = await _userManager.FindByIdAsync(dto.Id);
            if(currebtUser == null)
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "User not found",
                };
            }

            dto.Adapt(currebtUser);
           var result =  await _userManager.UpdateAsync(currebtUser);
            if(!result.Succeeded)
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = result.Errors.ToString()
                };
            }

            return new AuthDto
            {
                IsAuthenticated = true,
                Message = "User updated successfully!"
            };
            // _context.Entry(currebtUser).State = EntityState.Modified;
            
        }

        public async Task<AuthDto> DeleteUserAsync(string userId)
        {
            var currentUser = await _userManager.FindByIdAsync(userId);
            if (currentUser is null)
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "User not found"
                };

            var role = await _userManager.GetRolesAsync(currentUser);
            if (role.Contains("Admin"))
            {
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = "Cannot delete admin user"
                };

            }
            var result = await _userManager.DeleteAsync(currentUser);
            if (!result.Succeeded)
            {
                var errors = string.Empty;
                foreach (var error in result.Errors)
                {
                    errors += $"{error.Description}";
                }
                return new AuthDto
                {
                    IsAuthenticated = false,
                    Message = errors
                };
            }

            return new AuthDto
            {
                IsAuthenticated = true,
                Message = "User deleted successfully"
            };
        }
    }
}
