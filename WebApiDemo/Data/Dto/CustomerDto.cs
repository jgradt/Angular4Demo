using FluentValidation;

namespace WebApiDemo.Models
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }

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
}