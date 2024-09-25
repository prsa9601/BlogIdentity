using Blog.Infrastructure._Utilities.MediatR;
using Common.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Blog.Infrastructure.Persistent.Ef
{
    public class BlogContext : IdentityDbContext<Domain.UserAgg.User, Domain.RoleAgg.Role, string>
    {
        private readonly ICustomPublisher _publisher;
        public BlogContext(DbContextOptions<BlogContext> options, ICustomPublisher publisher) : base(options)
        {
            _publisher = publisher;
            //options.UseSqlServer(("DefaultConnection"),
            //    sqlServerOptionsAction: sqlOptions =>
            //    {
            //        sqlOptions.EnableRetryOnFailure(maxRetryCount: 5, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
            //    });
        }

       // public DbSet<Blog.Domain.UserAgg.User> User { get; set; }
        public DbSet<Blog.Domain.CategoryAgg.Category> Categories { get; set; }
        //public DbSet<Blog.Domain.RoleAgg.Role> Role { get; set; }
        public DbSet<Blog.Domain.PostAgg.Post> Posts { get; set; }
        public DbSet<Blog.Domain.CommentAgg.Comment> Comments { get; set; }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var modifiedEntities = GetModifiedEntities();
            await PublishEvents(modifiedEntities);
            return await base.SaveChangesAsync(cancellationToken);
        }
        private List<AggregateRoot> GetModifiedEntities() =>
            ChangeTracker.Entries<AggregateRoot>()
                .Where(x => x.State != EntityState.Detached)
                .Select(c => c.Entity)
                .Where(c => c.DomainEvents.Any()).ToList();

        private async Task PublishEvents(List<AggregateRoot> modifiedEntities)
        {
            foreach (var entity in modifiedEntities)
            {
                var events = entity.DomainEvents;
                foreach (var domainEvent in events)
                {
                    await _publisher.Publish(domainEvent, PublishStrategy.ParallelNoWait);
                }
            }
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BlogContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}

