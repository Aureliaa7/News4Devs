using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using News4Devs.Core.Entities;

namespace News4Devs.Infrastructure.ModelConfigurations
{
    class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(b => b.FirstName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(b => b.LastName)
                .IsRequired()
                .HasMaxLength(40);

            builder.Property(b => b.Email)
                .IsRequired()
                .HasMaxLength(30);

            builder.Property(b => b.PasswordHash)
                .IsRequired();

            builder.Property(b => b.PasswordSalt)
               .IsRequired();

            // the plaintext password won't be stored in DB 
            builder.Ignore(b => b.Password);
        }
    }
}
