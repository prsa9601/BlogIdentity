using Blog.Query.User.DTOs;
using Common.Query;

namespace Blog.Query.User.UserTokens.GetByJwtToken;

public record GetUserTokenByJwtTokenQuery(string HashJwtToken) : IQuery<UserTokenDto?>;