using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UniManagementSystem.Application.DTOs.DashboardDtos;
using UniManagementSystem.Application.Interfaces;
using UniManagementSystem.MVC.Models;

namespace UniManagementSystem.MVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        //get admin Dashboard 

        [Authorize]
        public IActionResult Index()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account");
            }
            return userRole?.ToLower() switch
            {
                "admin" => RedirectToAction("Admin"),
                "lecturer" => RedirectToAction("Lecturer"),
                "student" =>RedirectToAction("Student"),
                _ => RedirectToAction("Unauthorized")
            };
        }


        // Admin Dashboard
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Admin()
        {
            var result = await _dashboardService.GetAdminDashboardData();

            if (!result.IsAuthenticated || result.Data == null)
            {
                TempData["Error"] = result?.Message ?? "Failed to load admin dashboard.";
                return View("Error", new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier
                });
            }

            var model = result.Data as AdminDashboardDto;
            if (model == null)
            {
                TempData["Error"] = "Invalid admin dashboard data.";
                return View("Error", new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier
                });
            }

            return View("Admin", model);
        }



        // Lecturer Dashboard
        [Authorize(Roles = "Lecturer")]
        public async Task<IActionResult> Lecturer()
        {
            var lecturerId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(lecturerId))
                return RedirectToAction("Login", "Account");

            var result = await _dashboardService.GetLecturerDashboardDataAsync(lecturerId);

            if (!result.IsAuthenticated || result.Data == null)
            {
                TempData["Error"] = result?.Message ?? "Failed to load lecturer dashboard.";
                return View("Error", new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier
                });
            }

            var model = result.Data as LecturerDashboardDto;
            if (model == null)
            {
                TempData["Error"] = "Invalid lecturer dashboard data.";
                return View("Error", new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier
                });
            }

            return View("Lecturer", model);
        }

        [Authorize(Roles = "Student")]
        public async Task<IActionResult> Student()
        {
            var studentId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(studentId))
                return RedirectToAction("Login", "Account");

            var result = await _dashboardService.GetStudentDashboardDataAsync(studentId);

            if (!result.IsAuthenticated || result.Data == null)
            {
                TempData["Error"] = result?.Message ?? "Failed to load student dashboard.";
                return View("Error", new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier
                });
            }

            var model = result.Data as UniManagementSystem.Application.DTOs.DashboardDtos.StudentDashboardDto;
            if (model == null)
            {
                TempData["Error"] = "Invalid student dashboard data.";
                return View("Error", new ErrorViewModel
                {
                    RequestId = HttpContext.TraceIdentifier
                });
            }

            return View("Student", model);
        }

        public IActionResult Unauthorized()
        { return View(); }  


    }
}
