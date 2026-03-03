using System;
using System.Collections.Generic;
using System.Text;

namespace UniManagementSystem.Application.DTOs.ScheduleDto
{
    public class ScheduleStdDto
    {
        public string CourseName { get; set; }
        public string Day { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Lecturer { get; set; }
    }
}
