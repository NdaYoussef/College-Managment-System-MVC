using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Application.DTOs.DashboardDtos
{
    public class StudentDashboardDto
    {
        public StudentInfoDto StudentInfo { get; set; } = new StudentInfoDto();
        public int TotalCourses { get; set; }
        public List<CourseDto> Courses { get; set; } = new List<CourseDto>();
        public int UpcomingSchedules { get; set; }
        public List<ScheduleDtos> Schedules { get; set; } = new List<ScheduleDtos>();
    }
}
