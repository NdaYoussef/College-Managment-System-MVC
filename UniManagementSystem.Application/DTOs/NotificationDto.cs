using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Application.DTOs
{
    public class NotificationDto
    {
        public bool IsDeleted { get; set; } = false;
        public string Message { get; set; }
       // public DateTime Date { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
