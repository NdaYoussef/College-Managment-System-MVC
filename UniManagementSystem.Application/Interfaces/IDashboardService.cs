using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using UniManagementSystem.Application.DTOs;

namespace UniManagementSystem.Application.Interfaces
{
    public interface IDashboardService
    {
        //Admin methods
        Task<AuthDto> GetAdminDashboardData();
        //  Task<AuthDto> 

        // Lecturer Methods
        //Task<AuthDto> GetLecturerDashboardDataAsync(string lecturerId);
        //  Task<List<CourseDto>> GetLecturerCoursesAsync(string lecturerId);
       // Task<List<ScheduleDto>> GetUpcomingSchedulesAsync(string lecturerId);

        // Student Methods
        //Task<AuthDto> GetStudentDashboardDataAsync(string studentId);
        // Task<List<CourseDto>> GetStudentCoursesAsync(string studentId);
        // Task<StudentAcademicDto> GetStudentAcademicDataAsync(string studentId);

        //common methos 
    //    Task<List<NotificationDto>> GetRecentNotificationsAsync(string userId);
    }
}
