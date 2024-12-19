using Livraria.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Mapeamentos
{
    public class AssuntoMapeamento : IEntityTypeConfiguration<Assunto>
    {
        public void Configure(EntityTypeBuilder<Assunto> builder)
        {
            builder.ToTable(nameof(Assunto));

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Descricao)
                .HasColumnType("varchar(20)")
                .HasMaxLength(20)
                .IsRequired();

            builder.HasMany<LivroAssunto>()
                .WithOne(la => la.Assunto)
                .HasForeignKey(la => la.AssuntoId);
        }
    }
}
