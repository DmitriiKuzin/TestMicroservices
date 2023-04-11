using Mediator;
using Microsoft.Extensions.DependencyInjection;
using SecondService.Dto;
using SecondService.Requests;

namespace SecondService.Tests;

public class UserListTest
{
    private readonly IMediator _mediator;

    public UserListTest()
    {
        _mediator = new SecondServiceWebApplicationFactory()
            .Services
            .CreateScope()
            .ServiceProvider
            .GetRequiredService<IMediator>();
    }

    [Fact]
    public async Task GetUserFromOrganization_1()
    {
        var result = await _mediator.Send(new GetUsers
        {
            Page = 0,
            PageSize = 10,
            OrganizationId = 1
        });
        
        Assert.Equal(1, result.Total);
        Assert.Equal(new UserListItem
        {
            Id =  1,
            FirstName =  "Михаил",
            LastName =  "Зубенко",
            MiddleName =  "Петрович",
            PhoneNumber =  "8-800-555-35-35",
            Email =  "sus@gmail.com"
        },result.Users[0], new UserListItemEqualityComparer());
    }
    
    private class UserListItemEqualityComparer: IEqualityComparer<UserListItem>
    {
        public bool Equals(UserListItem x, UserListItem y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (ReferenceEquals(x, null)) return false;
            if (ReferenceEquals(y, null)) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Id == y.Id && x.FirstName == y.FirstName && x.LastName == y.LastName && x.MiddleName == y.MiddleName && x.PhoneNumber == y.PhoneNumber && x.Email == y.Email;
        }

        public int GetHashCode(UserListItem obj)
        {
            return HashCode.Combine(obj.Id, obj.FirstName, obj.LastName, obj.MiddleName, obj.PhoneNumber, obj.Email);
        }
    }
}