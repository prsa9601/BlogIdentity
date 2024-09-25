using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Blog.Infrastructure.Persistent.Ef.User
{
    public class UserConfiguration : IEntityTypeConfiguration<Domain.UserAgg.User>
    {
        public void Configure(EntityTypeBuilder<Domain.UserAgg.User> builder)
        {
            builder.ToTable("Users", "user");
            builder.HasIndex(b => b.PhoneNumber).IsUnique();

            builder.Property(b => b.PhoneNumber)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(b => b.Email)
                .IsUnique();
            
            builder.HasIndex(b => b.UserName)
                .IsUnique();
                

            builder.Property(b => b.Name)
                .IsRequired(false)
                .HasMaxLength(80);

            builder.Property(b => b.Family)
                .IsRequired(false)
                .HasMaxLength(80);

            builder.Property(b => b.UserName)
                .IsRequired()
                .HasMaxLength(80);

            builder.Property(b => b.Password)
                .IsRequired()
                .HasMaxLength(50);



            builder.OwnsMany(b => b.Tokens, option =>
            {
                option.ToTable("Tokens", "user");
                option.HasKey(b => b.Id);

                option.Property(b => b.HashJwtToken)
                    .IsRequired()
                    .HasMaxLength(250);

                option.Property(b => b.HashRefreshToken)
                    .IsRequired()
                    .HasMaxLength(250);

                option.Property(b => b.Device)
                    .IsRequired()
                    .HasMaxLength(100);
            });
           
            builder.OwnsMany(b => b.Roles, option =>
            {
                option.ToTable("Roles", "user");
                option.HasIndex(b => b.UserId);
            });
        }
    }
}
