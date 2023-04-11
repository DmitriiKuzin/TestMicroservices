using Common.Dto;
using FluentValidation;
namespace FirstService.Validation;

public class UserDtoValidator: AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(x => x.LastName)
            .NotEmpty();

        RuleFor(x => x.FirstName)
            .NotEmpty();

        RuleFor(x => x.Email)
            .NotEmpty();

        RuleFor(x => x.PhoneNumber)
            .NotEmpty();
    }
}