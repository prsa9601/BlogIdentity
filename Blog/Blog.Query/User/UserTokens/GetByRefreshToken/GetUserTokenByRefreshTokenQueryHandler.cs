using Common.Query;
using Dapper;
using Blog.Infrastructure.Persistent.Dapper;
using Blog.Query.User.DTOs;

namespace Blog.Query.User.UserTokens.GetByRefreshToken;

internal class GetUserTokenByRefreshTokenQueryHandler(DapperContext dapperContext) : IQueryHandler<GetUserTokenByRefreshTokenQuery, UserTokenDto?>
{
  


    public async Task<UserTokenDto?> Handle(GetUserTokenByRefreshTokenQuery request, CancellationToken cancellationToken)
    {
        using var connection = dapperContext.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM {dapperContext.UserTokens} Where HashRefreshToken=@hashRefreshToken";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new {hashRefreshToken = request.HashRefreshToken});
    }
}