using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace UniManagementSystem.Domain.BaseModel
{
    public class BaseClass<T>
    {
        public T Id { get; set; }   
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime CreatedBy { get; set; } 

        public DateTime  UpdatedAt { get; set; } 
        public DateTime  UpdatedBy { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
