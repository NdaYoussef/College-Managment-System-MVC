using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UniManagementSystem.Domain.BaseModel;
using UniManagementSystem.Domain.Enums;

namespace UniManagementSystem.Domain.Models
{
    public class Notification : BaseClass<int>
    {
        //public int Id { get; set; }
        [Required]
        public string Message { get; set; }
       // public DateTime Date { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    //    public Roles RecepetiveRole { get; set; }
        public ApplicationUser? User { get; set; }
        [ForeignKey("User")]
        public string? UserId { get; set; }

    }
}
