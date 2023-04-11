using DAL;
using Mediator;
using Microsoft.Extensions.DependencyInjection;
using Model;
using SecondService.Commands;

namespace SecondService.Tests;

public class UserBindTest
{
    private readonly IMediator _mediator;
    private readonly Context _context;

    public UserBindTest()
    {
        var sp = new SecondServiceWebApplicationFactory()
            .Services
            .CreateScope()
            .ServiceProvider;
        _mediator = sp
            .GetRequiredService<IMediator>();
        _context = sp
            .GetRequiredService<Context>();
    }

    [Fact]
    public async Task BindNewUserToOrganization_1()
    {
        using var trans = _context.Database.BeginTransaction();
        var user = new User
        {
            FirstName = "Test",
            LastName = "Test",
            PhoneNumber = "8888888888",
            Email = "bjhsdfbj@gmail.com"
        };

        _context.User.Add(user);
        _context.SaveChanges();
        _context.ChangeTracker.Clear();

        await _mediator.Send(new BindUserToOrganization()
        {
            OrganizationId = 1,
            UserId = user.Id
        });
        
        var resultUser = _context.User.First(x => x.Id == user.Id);
        Assert.Equal(1, resultUser.OrganizationId);
    }
}