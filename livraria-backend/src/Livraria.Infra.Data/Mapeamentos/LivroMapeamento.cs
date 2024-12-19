using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Livraria.Domain;

namespace Livraria.Infra.Data.Mapeamentos
{
    public class LivroMapeamento : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.ToTable(nameof(Livro));

            builder.HasKey(l => l.Id);

            builder.Property(l => l.Id)
                .ValueGeneratedOnAdd();

            builder.Property(l => l.Titulo)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(l => l.Editora)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.Property(l => l.Edicao)
                .IsRequired();

            builder.Property(l => l.AnoPublicacao)
                .HasColumnType("varchar(4)")
                .HasMaxLength(4)
                .IsRequired();
        }
    }
}
