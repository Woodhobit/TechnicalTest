using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserService.DAL.Models;

namespace UserService.DAL.Mapping
{
    public class UserProfileConfiguration : IEntityTypeConfiguration<UserProfile>
    {
        public void Configure(EntityTypeBuilder<UserProfile> builder)
        {
            builder.ToTable("Profiles");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.FirstName).HasMaxLength(255).IsRequired();
            builder.Property(c => c.SecondName).HasMaxLength(255).IsRequired();
            builder.Property(c => c.Email).HasMaxLength(127).IsRequired();

            builder.HasIndex(x => x.Id);
        }
    }
}
