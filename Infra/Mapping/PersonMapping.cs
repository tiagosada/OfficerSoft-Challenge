using Domain.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.Mapping
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.Property(s => s.Id)
                .IsRequired();

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.CPF)
                .IsRequired()
                .HasMaxLength(11);

            builder.HasIndex(s => s.CPF)
                .IsUnique();

            builder.Property(s => s.RG)
                .IsRequired();

            builder.HasIndex(s => s.RG)
                .IsUnique();

            builder.Property(s => s.CEP)
                .IsRequired();

            builder.Property(s => s.Address)
                .IsRequired();

            builder.Property(s => s.Number)
                .IsRequired();

            builder.Property(s => s.District)
                .IsRequired();

            builder.Property(s => s.Complement);

            builder.HasIndex(s => s.Id)
                .IsUnique();

        }
    }
}