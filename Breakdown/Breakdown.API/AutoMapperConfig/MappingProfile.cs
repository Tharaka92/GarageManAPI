using AutoMapper;
using Breakdown.API.ViewModels;
using Breakdown.API.ViewModels.Account;
using Breakdown.API.ViewModels.Package;
using Breakdown.API.ViewModels.Payment;
using Breakdown.API.ViewModels.Rating;
using Breakdown.API.ViewModels.Service;
using Breakdown.API.ViewModels.ServiceRequest;
using Breakdown.API.ViewModels.VehicleType;
using Breakdown.Contracts.Options;
using Breakdown.Domain.DTOs;
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
            CreateMap<Service, ServiceGetViewModel>().ReverseMap();
            CreateMap<Service, ServiceBaseViewModel>().ReverseMap();
            CreateMap<Service, ServiceUpdateViewModel>().ReverseMap();

            CreateMap<Package, PackageGetViewModel>().ReverseMap();
            CreateMap<Package, PackageBaseViewModel>().ReverseMap();
            CreateMap<Package, PackageUpdateViewModel>().ReverseMap();

            CreateMap<VehicleType, VehicleTypeGetViewModel>().ReverseMap();
            CreateMap<VehicleType, VehicleTypeBaseViewModel>().ReverseMap();
            CreateMap<VehicleType, VehicleTypeUpdateViewModel>().ReverseMap();

            CreateMap<ServiceRequest, ServiceRequestPostViewModel>().ReverseMap();

            CreateMap<Rating, RatingPostViewModel>().ReverseMap();

            CreateMap<ApplicationUser, PartnerViewModel>().ReverseMap();

            CreateMap<PartnerPayment, PartnerEarningsResponseViewModel>().ReverseMap();
            CreateMap<PartnerPayment, PartnerPaymentDto>().ReverseMap();
            CreateMap<PartnerPayment, OverDuePartnerFeeViewModel>().ReverseMap();
        }
    }
}
