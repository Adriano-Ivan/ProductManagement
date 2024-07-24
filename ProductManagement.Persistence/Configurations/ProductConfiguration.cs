using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Domain;

namespace ProductManagement.Persistence.Configurations;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasIndex(p => p.Descricao)
            .IsUnique();

        builder.Property(p => p.Descricao)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Marca)
            .IsRequired()
            .HasMaxLength(250);
    }
}
