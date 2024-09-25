using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;

namespace Blog.Application.Post.Show
{
    public record ShowPostCommand(long postId) : IBaseCommand;

}
