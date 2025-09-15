using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reto1_CleanArchitecture.Domain.Models;

namespace Reto1_CleanArchitecture.Infrastructure.Mappings
{
    public class AuthenticationsMap : IEntityTypeConfiguration<Authentications>
    {
        public void Configure(EntityTypeBuilder<Authentications> entity)
        {
            entity.ToTable("Authetication");

            entity.HasKey(e => e.AuthId);

            entity.Property(e => e.UserName)
                  .IsRequired()
                  .HasColumnType("TEXT(25)");

            entity.Property(e => e.Password)
                   .IsRequired()
                   .HasColumnType("TEXT(500)");
        }
    }
}
