using AutoMapper;
using Breakdown.API.ViewModels;
using Breakdown.Contracts.DTOs;
using Breakdown.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Breakdown.API.AutoMapperConfig
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Service, ServiceViewModel>().ReverseMap();
            CreateMap<Package, PackageViewModel>().ReverseMap();
            CreateMap<Vehicle, VehicleViewModel>().ReverseMap();
        }
    }
}
