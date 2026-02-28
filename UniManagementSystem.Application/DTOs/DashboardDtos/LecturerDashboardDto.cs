using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Application.DTOs.DashboardDtos
{
    public class LecturerDashboardDto
    {
        public string LecturerName { get; set; } = string.Empty;
        public int TotalCourses { get; set; }
        public int TotalStudents { get; set; }

       
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();

     
        public SystemStateDto SystemState { get; set; } = new SystemStateDto();
    }
}
