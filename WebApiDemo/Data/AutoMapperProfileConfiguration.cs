using WebApiDemo.Data.Entities;
using WebApiDemo.Models;
using AutoMapper;

namespace WebApiDemo.Data
{
    public class AutoMapperProfileConfiguration : Profile
    {
        public AutoMapperProfileConfiguration()
        {
            CreateMap<Customer, CustomerDto>()
                .ForSourceMember(m => m.CreatedDate, opt => opt.Ignore())
                .ForSourceMember(m => m.LastUpdatedDate, opt => opt.Ignore())
                .ReverseMap();
        }

    }
}
