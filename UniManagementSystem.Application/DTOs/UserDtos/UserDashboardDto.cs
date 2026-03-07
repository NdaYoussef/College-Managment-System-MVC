using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Domain.Enums;

namespace UniManagementSystem.Application.DTOs.UserDtos
{
    internal class UserDashboardDto
    {
        public string Id { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string? ProfilePic { get; set; }
        public string? NationalID { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int Age => (int)((DateTime.UtcNow - DateOfBirth).TotalDays / 365.25);
        public UserRoles Role { get; set; } = default!;
        public string? DepartmentName { get; set; }
        public double? GPA { get; set; }
        public decimal? Salary { get; set; }
        public int CoursesCount { get; set; }
    }
}
