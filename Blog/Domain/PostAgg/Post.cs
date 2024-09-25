using Blog.Domain.PostAgg.Services;
using Common.Domain;
using Common.Domain.Exceptions;
using Common.Domain.Utils;
using static System.Net.Mime.MediaTypeNames;

namespace Blog.Domain.PostAgg
{
    public class Post : BaseEntity
    {
        private Post()
        {
            
        }
        public Post(string userId, string imageName, string metaDescription, long categoryId, string title, string slug, string description, IPostDomainService service)
        {
            Guard(title, slug, service);
            UserId = userId;
            ImageName = imageName;
            CategoryId = categoryId;
            Title = title;
            Slug = slug.ToSlug();
            Description = description;
            Visit = 0;
            MetaDescription = metaDescription;
        }
        public string UserId { get; private set; }
        public long CategoryId { get; private set; }
        public string Title { get; private set; }
        public string ImageName { get; private set; }
        public string Slug { get; private set; }
        public string Description { get; private set; }
        public string MetaDescription { get; private set; }
        public int Visit { get; private set; }

        public void Edit(string title, string imageName, string metaDescription, string description, string slug, long categoryId, IPostDomainService service)
        {
            Guard(title, slug.ToSlug(), service);
            Title = title;
            Description = description;
            ImageName = imageName;
            Slug = slug.ToSlug();
            CategoryId = categoryId;
            MetaDescription = metaDescription;
        }
        public void AddImage(string image)
        {
            ImageName = image;
        }
        public void VisitPost()
        {
            Visit += 1;
        }
        public void Guard(string title, string slug, IPostDomainService service)
        {
            NullOrEmptyDomainDataException.CheckString(title, nameof(title));
            NullOrEmptyDomainDataException.CheckString(slug, nameof(slug));

            if (slug != Slug)
                if (service.IsSlugExist(slug))
                    throw new SlugIsDuplicateException();
        }
    }
}
