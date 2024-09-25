﻿using Common.Query;

namespace Blog.Query.User.DTOs;

public class UserTokenDto : BaseDto
{
    public string? UserId { get; set; }
    public string? HashJwtToken { get; set; }
    public string? HashRefreshToken { get; set; }
    public DateTime TokenExpireDate { get; set; }
    public DateTime RefreshTokenExpireDate { get; set; }
    public string? Device { get; set; }
}