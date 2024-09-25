using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;

namespace Blog.Application.Category.Create
{
    public class CreateCategoryCommand : IBaseCommand
    {
        public CreateCategoryCommand(string title, string metaTag, string metaDescription, string slug)
        {
            Title = title;
            MetaTag = metaTag;
            MetaDescription = metaDescription;
            Slug = slug;
        }
        public string Title { get; private set; }
        public string MetaTag { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
    }
}
