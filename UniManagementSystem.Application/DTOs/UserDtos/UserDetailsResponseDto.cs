using System;
using System.Collections.Generic;
using System.Text;

namespace UniManagementSystem.Application.DTOs.UserDtos
{
    public class UserDetailsResponseDto
    {
        public UserDashboardDto User { get; set; } 
        public IList<string> Roles { get; set; }
        public string DashboardRoute { get; set; }
    }
}
