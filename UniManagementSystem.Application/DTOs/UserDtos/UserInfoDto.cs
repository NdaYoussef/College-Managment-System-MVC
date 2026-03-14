using System;
using System.Collections.Generic;
using System.Text;

namespace UniManagementSystem.Application.DTOs.UserDtos
{
    public class UserInfoDto
    {
        public string UserId { get; set; }
        public string UserName { get; set; } = "N/A";
        public string Role {  get; set; }
    }
}
