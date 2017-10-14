using Angular2Demo.Data.Entities;
using Angular2Demo.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Angular2Demo.Data
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<Customer, CustomerModel>()
                .ForSourceMember(m => m.CreatedDate, opt => opt.Ignore())
                .ForSourceMember(m => m.LastUpdatedDate, opt => opt.Ignore())
                .ReverseMap();
        }

    }
}
