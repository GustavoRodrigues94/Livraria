using Livraria.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace Livraria.Infra.Data.Mapeamentos
{
    public class AutorMapeamento : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.ToTable(nameof(Autor));

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .ValueGeneratedOnAdd();

            builder.Property(a => a.Nome)
                .HasColumnType("varchar(40)")
                .HasMaxLength(40)
                .IsRequired();

            builder.HasMany<LivroAutor>()
                .WithOne(la => la.Autor)
                .HasForeignKey(la => la.AutorId);
        }
    }
}
