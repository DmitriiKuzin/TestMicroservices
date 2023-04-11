using Model;
using MQCommands;
using Riok.Mapperly.Abstractions;
using SecondService.Dto;

namespace SecondService.Mapping;

[Mapper]
public static partial class UserMapper
{
    public static partial User ToDbModel(this ICreateUser createUser);

    public static partial IQueryable<UserListItem> ProjectToDto(this IQueryable<User> users);
}