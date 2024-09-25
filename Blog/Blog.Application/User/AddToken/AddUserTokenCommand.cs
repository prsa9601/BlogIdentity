using Common.Application;

namespace Blog.Application.User.AddToken
{
    public class AddUserTokenCommand : IBaseCommand
    {
        public AddUserTokenCommand(string id, string hashJwtToken, string hashRefreshToken, DateTime tokenExpireDate, DateTime refreshTokenExpireDate, string device)
        {
            Id = id;
            HashJwtToken = hashJwtToken;
            HashRefreshToken = hashRefreshToken;
            TokenExpireDate = tokenExpireDate;
            RefreshTokenExpireDate = refreshTokenExpireDate;
            Device = device;
        }
        public string Id { get; }
        public string HashJwtToken { get; }
        public string HashRefreshToken { get; }
        public DateTime TokenExpireDate { get; }
        public DateTime RefreshTokenExpireDate { get; }
        public string Device { get; }
    }
}
