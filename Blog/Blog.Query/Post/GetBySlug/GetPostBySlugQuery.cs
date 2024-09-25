using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Query.Post.DTOs;
using Common.Query;

namespace Blog.Query.Post.GetBySlug
{
    public record class GetPostBySlugQuery(string slug) : IQuery<PostDto?>;

}
