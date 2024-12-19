﻿// <auto-generated />
using Livraria.Infra.Data.Contexto;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Livraria.Infra.Data.Migrations
{
    [DbContext(typeof(LivrariaDbContext))]
    [Migration("20241217130133_Initial-Migration")]
    partial class InitialMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Livraria.Domain.Assunto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Assunto", (string)null);
                });

            modelBuilder.Entity("Livraria.Domain.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Autor", (string)null);
                });

            modelBuilder.Entity("Livraria.Domain.Livro", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("AnoPublicacao")
                        .IsRequired()
                        .HasMaxLength(4)
                        .HasColumnType("nvarchar(4)");

                    b.Property<int>("Edicao")
                        .HasColumnType("int");

                    b.Property<string>("Editora")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.Property<string>("Titulo")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)");

                    b.HasKey("Id");

                    b.ToTable("Livro", (string)null);
                });

            modelBuilder.Entity("Livraria.Domain.LivroAssunto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AssuntoId")
                        .HasColumnType("int");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AssuntoId")
                        .HasDatabaseName("IX_LivroAssunto_Assunto");

                    b.HasIndex("LivroId")
                        .HasDatabaseName("IX_LivroAssunto_Livro");

                    b.ToTable("LivroAssunto", (string)null);
                });

            modelBuilder.Entity("Livraria.Domain.LivroAutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<int>("LivroId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("AutorId")
                        .HasDatabaseName("IX_LivroAutor_Autor");

                    b.HasIndex("LivroId")
                        .HasDatabaseName("IX_LivroAutor_Livro");

                    b.ToTable("LivroAutor", (string)null);
                });

            modelBuilder.Entity("Livraria.Domain.LivroAssunto", b =>
                {
                    b.HasOne("Livraria.Domain.Assunto", "Assunto")
                        .WithMany("LivrosAssuntos")
                        .HasForeignKey("AssuntoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Livraria.Domain.Livro", "Livro")
                        .WithMany("LivrosAssuntos")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Assunto");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("Livraria.Domain.LivroAutor", b =>
                {
                    b.HasOne("Livraria.Domain.Autor", "Autor")
                        .WithMany("LivrosAutores")
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Livraria.Domain.Livro", "Livro")
                        .WithMany("LivrosAutores")
                        .HasForeignKey("LivroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Livro");
                });

            modelBuilder.Entity("Livraria.Domain.Assunto", b =>
                {
                    b.Navigation("LivrosAssuntos");
                });

            modelBuilder.Entity("Livraria.Domain.Autor", b =>
                {
                    b.Navigation("LivrosAutores");
                });

            modelBuilder.Entity("Livraria.Domain.Livro", b =>
                {
                    b.Navigation("LivrosAssuntos");

                    b.Navigation("LivrosAutores");
                });
#pragma warning restore 612, 618
        }
    }
}