using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Domain.Enums;

namespace UniManagementSystem.Application.DTOs.UserDtos
{
    public class RegisterDto
    {
        [Required, MaxLength(50)]
        public string? FirstName { get; set; }

        [Required, MaxLength(50)]
        public string? LastName { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please confirm your password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Passwords do not match")]
        [Display(Name = "Confirm Password")]
        public string ConfirmedPassword { get; set; }

        [Display(Name = "National ID")]

        public string NationalID { get; set; }
    //    public string? Address { get; set; }
     //   public string? PhoneNumber { get; set; }
        public string Gender { get; set; }  

       // public DateTime DateOfBirth => 

        [Display(Name ="Person Type")]
        public Roles Role { get; set; }

    }
}
