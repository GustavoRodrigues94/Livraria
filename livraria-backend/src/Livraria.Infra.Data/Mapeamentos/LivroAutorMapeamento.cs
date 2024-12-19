using Livraria.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Mapeamentos
{
    public class LivroAutorMapeamento : IEntityTypeConfiguration<LivroAutor>
    {
        public void Configure(EntityTypeBuilder<LivroAutor> builder)
        {
            builder.ToTable(nameof(LivroAutor));

            builder.HasKey(la => la.Id);

            builder.Property(la => la.Id)
                .ValueGeneratedOnAdd();

            builder.Property(la => la.LivroId)
                .IsRequired();

            builder.Property(la => la.AutorId)
                .IsRequired();

            builder.HasIndex(la => la.LivroId)
                .HasDatabaseName("IX_LivroAutor_Livro");

            builder.HasIndex(la => la.AutorId)
                .HasDatabaseName("IX_LivroAutor_Autor");

            builder.HasOne(la => la.Livro)
                .WithMany(l => l.LivrosAutores)
                .HasForeignKey(la => la.LivroId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(la => la.Autor)
                .WithMany(a => a.LivrosAutores)
                .HasForeignKey(la => la.AutorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
