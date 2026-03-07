using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Application.DTOs.DashboardDtos
{
    public class AdminDashboardDto
    {
        public int TotalStudents { get; set; }
        public int TotalLecturers { get; set; }
        public int TotalCourses { get; set; }
        public int TotalDepartments { get; set; }
        public decimal TotalFees { get; set; }

        public List<NotificationDto> RecentNotifications { get; set; } = new();

        public List<CourseDto> RecentCourses { get; set; } = new();

        public SystemStateDto SystemState { get; set; } = new();
    }
}
