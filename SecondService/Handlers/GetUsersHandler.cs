using DAL;
using Mediator;
using Microsoft.EntityFrameworkCore;
using SecondService.Dto;
using SecondService.Mapping;
using SecondService.Requests;

namespace SecondService.Handlers;

public class GetUsersHandler: IRequestHandler<GetUsers, UserList>
{
    private readonly Context _context;

    public GetUsersHandler(Context context)
    {
        _context = context;
    }

    public async ValueTask<UserList> Handle(GetUsers request, CancellationToken cancellationToken)
    {
        var query = _context
            .User
            .Where(x => x.OrganizationId == request.OrganizationId);

        var total = await query.CountAsync(cancellationToken);

        var pageSize = request.PageSize == 0 ? total : request.PageSize;
        
        var users = await query
            .Skip(request.Page * pageSize)
            .Take(pageSize)
            .ProjectToDto()
            .ToListAsync(cancellationToken);

        return new UserList
        {
            Total = total,
            Users = users
        };
    }
}