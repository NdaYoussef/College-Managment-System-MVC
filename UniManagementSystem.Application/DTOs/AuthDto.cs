using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using UniManagementSystem.Domain.Enums;

namespace UniManagementSystem.Application.DTOs
{
    public class AuthDto
    {
        public string? Message { get; set; }
        public bool IsAuthenticated { get; set; } = false;
        public string? Token { get; set; }
        public object? Data { get; set; }
        public string? Email { get; set; }
      //  public List <Roles>? Role { get; set; }
        public string? UserName     { get; set; }
        public List<string>? Roles { get;  set; }
        [JsonIgnore]
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiration { get; set; }

        public string RedirectUrl { get; set; } 
    }
}
