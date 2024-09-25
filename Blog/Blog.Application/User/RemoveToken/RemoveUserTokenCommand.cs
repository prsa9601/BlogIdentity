using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;

namespace Blog.Application.User.RemoveToken
{
    public record RemoveUserTokenCommand(string UserName, long TokenId) : IBaseCommand<string>;

}
