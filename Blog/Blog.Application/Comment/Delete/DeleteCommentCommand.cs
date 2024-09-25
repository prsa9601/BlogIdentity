using Common.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Comment.Delete
{
    public record class DeleteCommentCommand(long commentId) : IBaseCommand;
   
}
