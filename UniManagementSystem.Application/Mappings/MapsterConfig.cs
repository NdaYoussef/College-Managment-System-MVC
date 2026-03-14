using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniManagementSystem.Application.DTOs.UserDtos;
using UniManagementSystem.Domain.Models;

namespace UniManagementSystem.Application.Mappings
{
    public  class MapsterConfig : IRegister
    {
        public  void Register(TypeAdapterConfig config) 
        {
            config.NewConfig<ApplicationUser, UserDashboardDto>();
            config.NewConfig<ApplicationUser, EditUserDto>();
            config.NewConfig<ApplicationUser, CreatUserDto>();
        }
    }
}
