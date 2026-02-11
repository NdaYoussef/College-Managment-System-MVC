using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using UniManagementSystem.Domain.Enums;

namespace UniManagementSystem.Domain.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required, MaxLength(50)]
        public string FirstName { get; set; }

        [Required, MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(250)]
        public string? Address { get; set; }
        public string Gender { get; set; }

        public string? ProfilePic { get; set; }
        [MaxLength(20)]
        public string NationalID { get; set; }
        public DateTime? DateOfBirth
        { 
            
           get
            {
                if (string.IsNullOrEmpty(NationalID) || NationalID.Length != 14)
                    return null;
                if (!int.TryParse(NationalID.Substring(1, 2), out int year)
                    || !int.TryParse(NationalID.Substring(3, 2), out int month)
                    || int.TryParse(NationalID.Substring(5, 2), out int day)) ;
                return null;

                int century = NationalID[0] == '2' ? 1900 :
                              NationalID[1] == '3' ?2000: 0;

                if(century ==0)
                    return null;
                if (!DateTime.TryParse($"{century + year}-{month}-{day}", out DateTime dob))
                    return null;
                return dob;
            }
        } 
        public double? GPA { get; set; }
        public Roles Role { get; set; }
        public decimal? Salary { get; set; }
        public bool IsDeleted { get; set; } = false;

        public Department? Department { get; set; }
        [ForeignKey("Department")]
        public int? DepartmentId { get; set; }
        
        public ICollection<UserCourse>? UserCourses { get; set; } = new List<UserCourse>();
        // [JsonIgnore]
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
    }
}
