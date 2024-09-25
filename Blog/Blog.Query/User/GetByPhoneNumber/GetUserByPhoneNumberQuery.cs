using Blog.Query.User.DTOs;
using Common.Query;

namespace Blog.Query.User.GetByPhoneNumber;

public record GetUserByPhoneNumberQuery(string PhoneNumber) : IQuery<UserDto?>;