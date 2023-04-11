using Common.Dto;
using FirstService.Validation;
using FluentValidation;

namespace FirstService.Tests;

public class UserDtoValidationTest
{
    private readonly IValidator<UserDto> _validator;
    private UserDto ValidUserDto = new UserDto
    {
        LastName = "Зубенко",
        FirstName = "Михаил",
        MiddleName = "Петрович",
        Email = "sas@gmail.com",
        PhoneNumber = "8-800-555-35-35"
    };

    public UserDtoValidationTest()
    {
        _validator = new UserDtoValidator();
    }
    
    [Fact]
    public void ValidUser_ReturnsTrue()
    {
        var validationResult = _validator.Validate(ValidUserDto);
        Assert.True(validationResult.IsValid);
    }

    [Fact]
    public void EmptyFirstName_ReturnsFalse()
    {
        ValidUserDto.FirstName = "";
        var validationResult = _validator.Validate(ValidUserDto);
        Assert.False(validationResult.IsValid);
    }
    [Fact]
    public void EmptyLastName_ReturnsFalse()
    {
        ValidUserDto.LastName = "";
        var validationResult = _validator.Validate(ValidUserDto);
        Assert.False(validationResult.IsValid);
    }
    [Fact]
    public void EmptyMiddleName_ReturnsTrue()
    {
        ValidUserDto.MiddleName = "";
        var validationResult = _validator.Validate(ValidUserDto);
        Assert.True(validationResult.IsValid);
    }
    [Fact]
    public void EmptyEmail_ReturnsFalse()
    {
        ValidUserDto.Email = "";
        var validationResult = _validator.Validate(ValidUserDto);
        Assert.False(validationResult.IsValid);
    }
    [Fact]
    public void EmptyPhoneNumber_ReturnsFalse()
    {
        ValidUserDto.PhoneNumber = "";
        var validationResult = _validator.Validate(ValidUserDto);
        Assert.False(validationResult.IsValid);
    }
}