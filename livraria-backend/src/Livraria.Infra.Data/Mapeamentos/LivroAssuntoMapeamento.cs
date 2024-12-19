using Livraria.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Mapeamentos
{
    public class LivroAssuntoMapeamento : IEntityTypeConfiguration<LivroAssunto>
    {
        public void Configure(EntityTypeBuilder<LivroAssunto> builder)
        {
            builder.ToTable(nameof(LivroAssunto));

            builder.HasKey(la => la.Id);

            builder.Property(la => la.Id)
                .ValueGeneratedOnAdd();

            builder.Property(la => la.LivroId)
                .IsRequired();

            builder.Property(la => la.AssuntoId)
                .IsRequired();

            builder.HasIndex(la => la.LivroId)
                .HasDatabaseName("IX_LivroAssunto_Livro");

            builder.HasIndex(la => la.AssuntoId)
                .HasDatabaseName("IX_LivroAssunto_Assunto");

            builder.HasOne(la => la.Livro)
                .WithMany(l => l.LivrosAssuntos)
                .HasForeignKey(la => la.LivroId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(la => la.Assunto)
                .WithMany(a => a.LivrosAssuntos)
                .HasForeignKey(la => la.AssuntoId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
