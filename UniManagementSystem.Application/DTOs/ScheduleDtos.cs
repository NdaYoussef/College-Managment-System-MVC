using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Application.DTOs
{
    public class ScheduleDtos
    {
        public string CourseName { get; set; } = string.Empty;
        public string Day { get; set; } = string.Empty;
        public string StartTime { get; set; } = string.Empty;
        public string EndTime { get; set; } = string.Empty;
        public string Lecturer { get; set; } = string.Empty;
    }
}
