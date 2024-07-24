using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManagement.Domain;

namespace ProductManagement.Persistence.Configurations;

public class ProviderConfiguration : IEntityTypeConfiguration<Provider>
{
    public void Configure(EntityTypeBuilder<Provider> builder)
    {
        builder.HasIndex(p => p.CNPJ)
            .IsUnique();

        builder.Property(p => p.CNPJ)
            .IsRequired()
            .HasMaxLength(60);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Endereco)
            .IsRequired()
            .HasMaxLength(250);

        builder.Property(p => p.Telefone)
            .IsRequired()
            .HasMaxLength(250);

        builder.HasMany(p => p.Products)
            .WithOne(p => p.Provider)
            .HasForeignKey(x => x.ProviderId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
