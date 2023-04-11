using MQCommands;
using DAL;
using MassTransit;
using SecondService.Mapping;

namespace SecondService.Consumers;

public class CreateUserConsumer: IConsumer<ICreateUser>
{
    private readonly Context _context;

    public CreateUserConsumer(Context context)
    {
        _context = context;
    }

    public async Task Consume(ConsumeContext<ICreateUser> context)
    {
        var user = context.Message.ToDbModel();
        _context.User.Add(user);
        await _context.SaveChangesAsync();
    }
}