using System;
using System.Collections.Generic;
using System.Text;

namespace UniManagementSystem.Application.DTOs.UserDtos
{
    public class CreatUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string Gender { get; set; }
        public string? Address { get; set; }
        public string NationalID { get; set; }
        public double? GPA { get; set; }
        public decimal? Salary { get; set; }
        public int? DepartmentId { get; set; }
        public string Role { get; set; } = "Student";
    }
}
