using DAL;
using Mediator;
using Model;
using SecondService.Commands;

namespace SecondService.Handlers;

public class BindUserToOrganizationHandler: ICommandHandler<BindUserToOrganization>
{
    private readonly Context _context;

    public BindUserToOrganizationHandler(Context context)
    {
        _context = context;
    }

    public async ValueTask<Unit> Handle(BindUserToOrganization command, CancellationToken cancellationToken)
    {
        var user = new User { Id = command.UserId};
        _context.User.Attach(user);
        user.OrganizationId = command.OrganizationId;
        await _context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}