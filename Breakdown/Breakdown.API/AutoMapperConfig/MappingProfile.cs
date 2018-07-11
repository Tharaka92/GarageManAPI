using AutoMapper;
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
            CreateMap<Package, PackageDto>().ReverseMap();
        }
    }
}
