using System.ComponentModel.DataAnnotations.Schema;
using UniManagementSystem.Domain.BaseModel;

namespace UniManagementSystem.Domain.Models
{
    public class Schedule : BaseClass<int>
    {
        //public int Id { get; set; }
        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; } = TimeSpan.Zero;
        public TimeSpan EndTime { get; set; } = TimeSpan.Zero;
        public Course Course { get; set; }
        [ForeignKey("Course")]
        public int CourseId { get; set; }
        public ApplicationUser Lecturer { get; set; }
        [ForeignKey("Lecturer")]
        public string LecturerId { get; set; }

    }
}
