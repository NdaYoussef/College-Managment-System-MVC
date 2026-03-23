using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Application.DTOs;
using UniManagementSystem.Application.DTOs.UserDtos;
using UniManagementSystem.Domain.Models;

namespace UniManagementSystem.Application.Interfaces
{
    public interface IUserService
    {
        Task<AuthDto> GetAllUsers();
        Task<AuthDto> GetUserData(string userId);
        Task<AuthDto> CreateUserAsync(CreatUserDto dto);

        Task<AuthDto> EditUserAsync(EditUserDto dto);
        Task<AuthDto> DeleteUserAsync(string userId);
        Task<AuthDto> ChangeProfilePictureAsync(string userId,  IFormFile ProfileImage);
        Task<AuthDto> ChangeUserRoleAsync(string userId, string newRole);
        Task<(bool IsSuccess, string message)> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
    }
}
