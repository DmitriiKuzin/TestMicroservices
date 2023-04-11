using MQCommands;

namespace Common.Dto;

public class UserDto: ICreateUser
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string PhoneNumber { get; set; }
    public string Email { get; set; }
}