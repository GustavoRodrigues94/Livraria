using Livraria.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Mapeamentos
{
    public class LivroValorCompraMapeamento : IEntityTypeConfiguration<LivroValorCompra>
    {
        public void Configure(EntityTypeBuilder<LivroValorCompra> builder)
        {
            builder.ToTable(nameof(LivroValorCompra));

            builder.HasKey(lvc => lvc.Id);

            builder.Property(lvc => lvc.Id)
                .ValueGeneratedOnAdd();

            builder.Property(lvc => lvc.LivroId)
                .IsRequired();

            builder.HasOne(lvc => lvc.Livro)
                .WithMany(l => l.LivroValoresCompra)
                .HasForeignKey(lvc => lvc.LivroId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Property(lvc => lvc.Tipo)
                .IsRequired()
                .HasConversion<int>()
                .HasColumnType("int");

            builder.Property(lvc => lvc.Valor)
                .IsRequired()
                .HasColumnType("decimal(18,2)");
        }
    }
}
