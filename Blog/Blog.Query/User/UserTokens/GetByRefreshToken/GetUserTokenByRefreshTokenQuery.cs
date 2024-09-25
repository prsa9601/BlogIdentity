using Blog.Query.User.DTOs;
using Common.Query;

namespace Blog.Query.User.UserTokens.GetByRefreshToken;

public record GetUserTokenByRefreshTokenQuery(string HashRefreshToken) : IQuery<UserTokenDto?>;