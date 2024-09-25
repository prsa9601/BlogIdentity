using Blog.Domain.CategoryAgg.Repository;
using Blog.Domain.CommentAgg.Repository;
using Blog.Domain.PostAgg.Repository;
using Blog.Domain.RoleAgg;
using Blog.Domain.RoleAgg.Repository;
using Blog.Domain.UserAgg;
using Blog.Domain.UserAgg.Repository;
using Blog.Infrastructure._Utilities.MediatR;
using Blog.Infrastructure.Persistent.Dapper;
using Blog.Infrastructure.Persistent.Ef;
using Blog.Infrastructure.Persistent.Ef.Category;
using Blog.Infrastructure.Persistent.Ef.Comment;
using Blog.Infrastructure.Persistent.Ef.Post;
using Blog.Infrastructure.Persistent.Ef.Role;
using Blog.Infrastructure.Persistent.Ef.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace Blog.Infrastructure
{
    public class InfrastructureBootstrapper
    {
        public static void Init(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICategoryRepository, CategoryRepository>();
            services.AddTransient<IRoleRepository<Blog.Domain.RoleAgg.Role>, RoleRepository<Role>>();
            services.AddTransient<IUserRepository<Blog.Domain.UserAgg.User>, UserRepository<User>>();
            services.AddTransient<ICommentRepository, CommentRepository>();
            services.AddTransient<IPostRepository, PostRepository>();

            services.AddSingleton<ICustomPublisher, CustomPublisher>();

            services.AddTransient(_ => new DapperContext(connectionString));
            services.AddIdentity<Blog.Domain.UserAgg.User, Blog.Domain.RoleAgg.Role>(options =>
                {

                    // User Options
                    // options.User.RequireUniqueEmail = true;
                    // options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+";
                    //options.User.AllowedUserNameCharacters =
                    //    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+";
                    // options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+";
                    options.User.AllowedUserNameCharacters =
                        "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/ ";

                    //options.SignIn.RequireConfirmedAccount = true;  
                    // Signin Options
                  
                    options.SignIn.RequireConfirmedAccount = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.SignIn.RequireConfirmedPhoneNumber = true;
                    //// Password Options
                    options.Password.RequireUppercase = false;
                    options.Password.RequiredUniqueChars = 0;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
                    //// LockOut
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(3);
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    // Stores Options
                    //options.Stores.MaxLengthForKeys = 10;
                    //options.Stores.ProtectPersonalData = false;
                    options.Lockout.MaxFailedAccessAttempts = 3;
                    options.Stores.ProtectPersonalData = false;
                    //options.Tokens.AuthenticatorTokenProvider = "";
                    options.Stores.ProtectPersonalData = false;
                    //options.ClaimsIdentity.UserNameClaimType = "ClaimTypes.Name";
                    //options.ClaimsIdentity.UserIdClaimType = "ClaimTypes.NameIdentifier";
                    //options.ClaimsIdentity.EmailClaimType = "ClaimTypes.Email";
                    //options.ClaimsIdentity.UserNameClaimType = "ClaimTypes.MobilePhone";
                    
                })
                .AddEntityFrameworkStores<BlogContext>().
            AddDefaultTokenProviders();
            //.AddErrorDescriber<PersianIdentityErrors>();
            //remind
            services.AddDbContext<BlogContext>(option =>
            {
                option.UseSqlServer(connectionString);
            });
        }
    }
}