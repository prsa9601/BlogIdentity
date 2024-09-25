using Blog.Presentation.Facade.Category;
using Blog.Presentation.Facade.Comment;
using Blog.Presentation.Facade.Post;
using Blog.Presentation.Facade.Role;
using Blog.Presentation.Facade.User;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Presentation.Facade
{
    public static class FacadeBootstrapper
    {
        public static void InitFacadeDependency(this IServiceCollection services)
        {
            services.AddScoped<ICategoryFacade, CategoryFacade>();
            services.AddScoped<ICommentFacade, CommentFacade>();
            services.AddScoped<IRoleFacade, RoleFacade>();
            services.AddScoped<IPostFacade, PostFacade>();

            services.AddScoped<IUserFacade, UserFacade>();

        }
    }
}