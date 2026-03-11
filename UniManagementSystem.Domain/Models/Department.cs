using System.ComponentModel.DataAnnotations;
using UniManagementSystem.Domain.BaseModel;

namespace UniManagementSystem.Domain.Models
{
    public class Department : BaseClass<int>
    {
        //public int Id { get; set; }
        [Required, MaxLength(100)]
        public string Name { get; set; }

        public ICollection<ApplicationUser>? Users { get; set; } = new List<ApplicationUser>();
        public ICollection<Course> Courses { get; set; }= new List<Course>();
    }
}
