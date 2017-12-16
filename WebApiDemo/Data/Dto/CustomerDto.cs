using AutoMapper;
using FluentValidation;
using WebApiDemo.Data.Entities;

namespace WebApiDemo.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

    // Fluent Validation
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.FirstName).NotNull().Length(1, 50);
            RuleFor(x => x.LastName).NotNull().Length(1, 50);
            RuleFor(x => x.Email).EmailAddress().MaximumLength(150);
        }
    }

    // AutoMapper
    public class CustomerAutoMapProfile : Profile
    {
        public CustomerAutoMapProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForSourceMember(m => m.CreatedDate, opt => opt.Ignore())
                .ForSourceMember(m => m.LastUpdatedDate, opt => opt.Ignore())
                .ForSourceMember(m => m.Orders, opt => opt.Ignore())
                .ReverseMap();
        }

    }
}