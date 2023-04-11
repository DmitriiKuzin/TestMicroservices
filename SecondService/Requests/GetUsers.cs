using Mediator;
using SecondService.Dto;

namespace SecondService.Requests;

public class GetUsers: IRequest<UserList>
{
    public int Page { get; set; }

    public int PageSize { get; set; }
    public int OrganizationId { get; set; }
}