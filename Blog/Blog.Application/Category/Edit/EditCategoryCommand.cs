using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Application;

namespace Blog.Application.Category.Edit
{
    public class EditCategoryCommand : IBaseCommand
    {
        public EditCategoryCommand(long id, string title, string metaTag, string metaDescription, string slug)
        {
            Id = id;
            Title = title;
            MetaTag = metaTag;
            MetaDescription = metaDescription;
            Slug = slug;
        }
        public long Id { get; set; }
        public string Title { get; private set; }
        public string MetaTag { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }
    }
}
