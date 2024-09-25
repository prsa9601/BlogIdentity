using Blog.Application._Utilities;
using Blog.Application.Category;
using Blog.Application.Post;
using Blog.Application.Role.Create;
using Blog.Application.User;
using Blog.Application.User.Create;
using Blog.Domain.CategoryAgg.Services;
using Blog.Domain.PostAgg.Services;
using Blog.Domain.UserAgg.Services;
using Blog.Infrastructure;
using Blog.Presentation.Facade;
using Blog.Query.Category.GetById;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Blog.Config
{
    public static class BlogBootstrapper
    {
        public static void RegisterBlogDependency(this IServiceCollection services, string connectionString)
        {
            InfrastructureBootstrapper.Init(services, connectionString);

            services.AddMediatR(typeof(CreateUserCommandHandler).Assembly);
            services.AddMediatR(typeof(Directories).Assembly);

            services.AddMediatR(typeof(GetCategoryByIdQuery).Assembly);

            services.AddTransient<IPostDomainService, PostDomainService>();
            services.AddTransient<IUserDomainService, UserDomainService>();
            services.AddTransient<ICategoryDomainService, CategoryDomainService>();
            //services.AddTransient<IUserDomainService, UserDomainService>();


            services.AddValidatorsFromAssembly(typeof(CreateRoleCommandValidator).Assembly);

            services.InitFacadeDependency();
        }
    }
}