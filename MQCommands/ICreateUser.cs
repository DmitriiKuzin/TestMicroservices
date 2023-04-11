namespace MQCommands;

public interface ICreateUser
{
    public string FirstName { get; }
    public string LastName { get; }
    public string MiddleName { get; }
    public string PhoneNumber { get; }
    public string Email { get; }
}