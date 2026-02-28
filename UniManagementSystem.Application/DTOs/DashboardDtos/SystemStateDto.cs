using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Application.DTOs.DashboardDtos
{
    public class SystemStateDto
    {
        public int TotalUsers { get; set; }
        // public long StoredUsage { get; set; } // لو هتحسبي storage
        public DateTime LastUpdated { get; set; }

        public string ServerUptime { get; set; } = "Unknown";   // مدة تشغيل السيرفر
        public int ActiveUsers { get; set; } = 0;               // عدد المستخدمين النشطين حاليا
        public string DatabaseStatus { get; set; } = "Unknown";
    }
}
