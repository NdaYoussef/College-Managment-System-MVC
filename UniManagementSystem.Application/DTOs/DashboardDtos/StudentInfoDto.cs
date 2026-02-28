using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Application.DTOs.DashboardDtos
{
    public class StudentInfoDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Department { get; set; } = string.Empty;
        public double GPA { get; set; }
        public string? ProfilePic { get; set; }
    }
}
