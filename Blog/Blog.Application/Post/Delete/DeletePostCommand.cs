using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Post.Delete
{
    public record class DeletePostCommand(long postId):IBaseCommand;
}
