using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Reto1_CleanArchitecture.Domain.Models;

namespace Reto1_CleanArchitecture.Infrastructure.Mappings
{
    public class ContactsMap : IEntityTypeConfiguration<Contacts>
    {
        public void Configure(EntityTypeBuilder<Contacts> entity)
        {
            entity.ToTable("Contacts");

            entity.HasKey(e => e.ContactId);

            entity.Property(e => e.Name)
                  .IsRequired()
                  .HasColumnType("TEXT(50)");

            entity.Property(e => e.Phone)
                  .IsRequired()
                  .HasColumnType("TEXT(50)");

            entity.Property(e => e.Email)
                  .HasColumnType("TEXT(50)");

            entity.Property(e => e.Company)
                  .HasColumnType("TEXT(50)");

            entity.Property(e => e.Address)
                  .HasColumnType("TEXT(150)");

            entity.Property(e => e.Notes)
                  .HasColumnType("TEXT(300)");

            entity.Property(e => e.Status)
                    .IsRequired()
                    .HasColumnType("INTEGER")
                    .HasDefaultValue(1);

            entity.Property(e => e.CreatedBy)
                  .HasColumnType("TEXT(25)");

            entity.Property(e => e.CreatedDate)
                  .HasColumnType("TEXT(25)");

            entity.Property(e => e.UpdatedBy)
                  .HasColumnType("TEXT(25)");

            entity.Property(e => e.StatusChangedDate)
                  .HasColumnType("TEXT(25)");

            entity.Property(e => e.StatusChangedDate)
                  .HasColumnType("TEXT(25)");
        }
    }
}
