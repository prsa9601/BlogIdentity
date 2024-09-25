using Blog.Infrastructure.Persistent.Dapper;
using Blog.Query.User.DTOs;
using Common.Query;
using Dapper;

namespace Blog.Query.User.UserTokens.GetByJwtToken;

internal class GetUserTokenByJwtTokenQueryHandler(DapperContext dapperContext)
    : IQueryHandler<GetUserTokenByJwtTokenQuery, UserTokenDto?>
{
    public async Task<UserTokenDto?> Handle(GetUserTokenByJwtTokenQuery request, CancellationToken cancellationToken)
    {
        using var connection = dapperContext.CreateConnection();
        var sql = $"SELECT TOP(1) * FROM {dapperContext.UserTokens} Where HashJwtToken=@hashJwtToken";
        return await connection.QueryFirstOrDefaultAsync<UserTokenDto>(sql, new { hashJwtToken = request.HashJwtToken });
    }
}