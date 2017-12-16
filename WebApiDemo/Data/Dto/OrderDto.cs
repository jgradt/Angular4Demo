using AutoMapper;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiDemo.Data.Entities;

namespace WebApiDemo.Data.Dto
{
    public class OrderDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public DateTime? DeliveredDate { get; set; }
        public DateTime OrderDate { get; set; }
        public OrderStatus Status { get; set; }
        public decimal TotalDue { get; set; }
        public string Comment { get; set; }
    }

    // Fluent Validation
    public class OrderDtoValidator : AbstractValidator<OrderDto>
    {
        public OrderDtoValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.TotalDue).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(x => x.Comment).Length(0, 2000);
        }
    }

    // AutoMapper
    public class OrderAutoMapProfile : Profile
    {
        public OrderAutoMapProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForSourceMember(m => m.CreatedDate, opt => opt.Ignore())
                .ForSourceMember(m => m.LastUpdatedDate, opt => opt.Ignore())
                .ForSourceMember(m => m.Customer, opt => opt.Ignore())
                .ReverseMap();
        }

    }

}
