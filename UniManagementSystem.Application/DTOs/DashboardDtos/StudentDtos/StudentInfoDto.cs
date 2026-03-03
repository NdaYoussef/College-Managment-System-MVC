using System;
using System.Collections.Generic;
using System.Text;

namespace UniManagementSystem.Application.DTOs.DashboardDtos.StudentDtos
{
    public class StudentInfoDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public double GPA { get; set; }
        public string ProfilePic { get; set; }
    }
}
