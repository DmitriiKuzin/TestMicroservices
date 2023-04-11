using Mediator;

namespace SecondService.Commands;

public class BindUserToOrganization: ICommand
{
    public int UserId { get; set; }

    public int OrganizationId { get; set; }
}