using Blog.Domain.CategoryAgg.Services;
using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;

namespace Blog.Domain.CategoryAgg
{
    public class Category : BaseEntity
    {
        private Category()
        {
            
        }
        public Category(string title, string metaTag, string metaDescription, string slug
            ,  ICategoryDomainService service)
        {
            Slug = slug.ToSlug();
            Guard(title, slug.ToSlug(), service);
            Title = title;
            MetaTag = metaTag;
            MetaDescription = metaDescription;
        }
        public string Title { get; private set; }
        public string MetaTag { get; private set; }
        public string MetaDescription { get; private set; }
        public string Slug { get; private set; }

        public void Edit(string title, string slug, string metaTag,string metaDescription, ICategoryDomainService service)
        {
            Slug = slug.ToSlug();
            Guard(title, slug, service);
            Title = title;
            MetaTag = metaTag;
            MetaDescription = metaDescription;
        }
        public void Guard(string title, string slug, ICategoryDomainService service)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

            if (slug != Slug)
                if (service.IsSlugExist(slug))
                    throw new SlugIsDuplicateException();
            if (title != Title)
                if (service.IsSlugExist(title))
                    throw new Exception("دسته بندی با این نام وجود دارد!");
        }
    }
}
