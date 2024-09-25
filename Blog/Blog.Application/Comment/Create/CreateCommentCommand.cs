using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;

namespace Blog.Application.Comment.Create
{
    public record CreateCommentCommand(string Text, string UserId, long ProductId) : IBaseCommand;

}
