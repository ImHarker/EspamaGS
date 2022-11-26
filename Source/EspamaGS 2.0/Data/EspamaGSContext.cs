using System;
using System.Collections.Generic;
using EspamaGS_2._0.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EspamaGS_2._0.Data {
    public partial class EspamaGSContext : IdentityDbContext {
        public EspamaGSContext() {
        }

        public EspamaGSContext(DbContextOptions<EspamaGSContext> options)
            : base(options) {
        }

        public virtual DbSet<Administrador> Administradors { get; set; } = null!;
        public virtual DbSet<Categoria> Categoria { get; set; } = null!;
        public virtual DbSet<Cliente> Clientes { get; set; } = null!;
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Desenvolvedora> Desenvolvedoras { get; set; } = null!;
        public virtual DbSet<Funcionario> Funcionarios { get; set; } = null!;
        public virtual DbSet<Jogo> Jogos { get; set; } = null!;
        public virtual DbSet<Plataforma> Plataformas { get; set; } = null!;
        public virtual DbSet<Utilizador> Utilizadors { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EspamaGS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Administrador>(entity => {
                entity.HasKey(e => e.IdUtilizador)
                    .HasName("PK__Administ__7BC3137C7059CC98");

                entity.ToTable("Administrador");

                entity.Property(e => e.IdUtilizador)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_UTILIZADOR");

                entity.Property(e => e.DataRegisto)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_REGISTO");

                entity.Property(e => e.IdAdmin).HasColumnName("ID_ADMIN");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.InverseIdAdminNavigation)
                    .HasForeignKey(d => d.IdAdmin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Administr__ID_AD__30F848ED");

                entity.HasOne(d => d.IdUtilizadorNavigation)
                    .WithOne(p => p.Administrador)
                    .HasForeignKey<Administrador>(d => d.IdUtilizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Administr__ID_UT__31EC6D26");
            });

            modelBuilder.Entity<Categoria>(entity => {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nome)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("NOME");
            });

            modelBuilder.Entity<Cliente>(entity => {
                entity.HasKey(e => e.IdUtilizador)
                    .HasName("PK__Cliente__7BC3137C9FB89D4F");

                entity.ToTable("Cliente");

                entity.Property(e => e.IdUtilizador)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_UTILIZADOR");

                entity.Property(e => e.Estado).HasColumnName("ESTADO");

                entity.HasOne(d => d.IdUtilizadorNavigation)
                    .WithOne(p => p.Cliente)
                    .HasForeignKey<Cliente>(d => d.IdUtilizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Cliente__ID_UTIL__286302EC");

                entity.HasMany(d => d.IdCategoria)
                    .WithMany(p => p.IdClientes)
                    .UsingEntity<Dictionary<string, object>>(
                        "Preferencium",
                        l => l.HasOne<Categoria>().WithMany().HasForeignKey("IdCategoria").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Preferenc__ID_CA__3F466844"),
                        r => r.HasOne<Cliente>().WithMany().HasForeignKey("IdCliente").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__Preferenc__ID_CL__3E52440B"),
                        j => {
                            j.HasKey("IdCliente", "IdCategoria").HasName("PK__Preferen__671E10CA6AF680E2");

                            j.ToTable("Preferencia");

                            j.IndexerProperty<int>("IdCliente").HasColumnName("ID_CLIENTE");

                            j.IndexerProperty<int>("IdCategoria").HasColumnName("ID_CATEGORIA");
                        });
            });

            modelBuilder.Entity<Compra>(entity => {
                entity.HasKey(e => new { e.IdCliente, e.IdJogo })
                    .HasName("PK__Compra__6B235F1E512F019E");

                entity.ToTable("Compra");

                entity.Property(e => e.IdCliente).HasColumnName("ID_CLIENTE");

                entity.Property(e => e.IdJogo).HasColumnName("ID_JOGO");

                entity.Property(e => e.DataCompra)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_COMPRA");

                entity.Property(e => e.Preco)
                    .HasColumnType("money")
                    .HasColumnName("PRECO");

                entity.HasOne(d => d.IdClienteNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdCliente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compra__ID_CLIEN__4222D4EF");

                entity.HasOne(d => d.IdJogoNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdJogo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compra__ID_JOGO__4316F928");
            });

            modelBuilder.Entity<Desenvolvedora>(entity => {
                entity.ToTable("Desenvolvedora");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOME");
            });

            modelBuilder.Entity<Funcionario>(entity => {
                entity.HasKey(e => e.IdUtilizador)
                    .HasName("PK__Funciona__7BC3137CB86228DC");

                entity.ToTable("Funcionario");

                entity.Property(e => e.IdUtilizador)
                    .ValueGeneratedNever()
                    .HasColumnName("ID_UTILIZADOR");

                entity.Property(e => e.DataRegisto)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_REGISTO");

                entity.Property(e => e.IdAdmin).HasColumnName("ID_ADMIN");

                entity.Property(e => e.Telefone)
                    .HasMaxLength(9)
                    .IsUnicode(false)
                    .HasColumnName("TELEFONE");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.IdAdmin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Funcionar__ID_AD__34C8D9D1");

                entity.HasOne(d => d.IdUtilizadorNavigation)
                    .WithOne(p => p.Funcionario)
                    .HasForeignKey<Funcionario>(d => d.IdUtilizador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Funcionar__ID_UT__35BCFE0A");
            });

            modelBuilder.Entity<Jogo>(entity => {
                entity.ToTable("Jogo");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.DataLancamento)
                    .HasColumnType("date")
                    .HasColumnName("DATA_LANCAMENTO");

                entity.Property(e => e.DataRegisto)
                    .HasColumnType("datetime")
                    .HasColumnName("DATA_REGISTO");

                entity.Property(e => e.Descricao)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("DESCRICAO");

                entity.Property(e => e.Foto)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("FOTO");

                entity.Property(e => e.IdCategoria).HasColumnName("ID_CATEGORIA");

                entity.Property(e => e.IdDesenvolvedora).HasColumnName("ID_DESENVOLVEDORA");

                entity.Property(e => e.IdFuncionario).HasColumnName("ID_FUNCIONARIO");

                entity.Property(e => e.IdPlataforma).HasColumnName("ID_PLATAFORMA");

                entity.Property(e => e.Nome)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("NOME");

                entity.Property(e => e.Preco)
                    .HasColumnType("money")
                    .HasColumnName("PRECO");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__ID_CATEGOR__38996AB5");

                entity.HasOne(d => d.IdDesenvolvedoraNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdDesenvolvedora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__ID_DESENVO__398D8EEE");

                entity.HasOne(d => d.IdFuncionarioNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdFuncionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__ID_FUNCION__3B75D760");

                entity.HasOne(d => d.IdPlataformaNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdPlataforma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__ID_PLATAFO__3A81B327");
            });

            modelBuilder.Entity<Plataforma>(entity => {
                entity.ToTable("Plataforma");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Nome)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("NOME");
            });

            modelBuilder.Entity<Utilizador>(entity => {
                entity.ToTable("Utilizador");

                entity.HasIndex(e => e.Email, "UQ__Utilizad__161CF7241F614C5A")
                    .IsUnique();

                entity.HasIndex(e => e.Username, "UQ__Utilizad__B15BE12EB4738E14")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Email)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("EMAIL");

                entity.Property(e => e.Nome)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("NOME");

                entity.Property(e => e.Passwd)
                    .HasMaxLength(64)
                    .IsUnicode(false)
                    .HasColumnName("PASSWD");

                entity.Property(e => e.Username)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("USERNAME");
            });

            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
