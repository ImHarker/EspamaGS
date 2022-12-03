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
        public virtual DbSet<Compra> Compras { get; set; } = null!;
        public virtual DbSet<Desenvolvedora> Desenvolvedoras { get; set; } = null!;
        public virtual DbSet<Funcionario> Funcionarios { get; set; } = null!;
        public virtual DbSet<Jogo> Jogos { get; set; } = null!;
        public virtual DbSet<Plataforma> Plataformas { get; set; } = null!;
        public virtual DbSet<Preferencia> Preferencia { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
            if (!optionsBuilder.IsConfigured) {
                optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=EspamaGS;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<Administrador>(entity => {
                entity.HasKey(e => e.IdUtilizador)
                    .HasName("PK__Administ__7BC3137CC20F3687");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.InverseIdAdminNavigation)
                    .HasForeignKey(d => d.IdAdmin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Administr__ID_AD__1332DBDC");
            });

            modelBuilder.Entity<Compra>(entity => {
                entity.HasKey(e => new { e.IdCliente, e.IdJogo })
                    .HasName("PK__Compra__6B235F1E45A1618F");

                entity.HasOne(d => d.IdJogoNavigation)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdJogo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Compra__ID_JOGO__2180FB33");
            });

            modelBuilder.Entity<Funcionario>(entity => {
                entity.HasKey(e => e.IdUtilizador)
                    .HasName("PK__Funciona__7BC3137C400E1255");

                entity.HasOne(d => d.IdAdminNavigation)
                    .WithMany(p => p.Funcionarios)
                    .HasForeignKey(d => d.IdAdmin)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Funcionar__ID_AD__160F4887");
            });

            modelBuilder.Entity<Jogo>(entity => {
                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__ID_CATEGOR__18EBB532");

                entity.HasOne(d => d.IdDesenvolvedoraNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdDesenvolvedora)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__ID_DESENVO__19DFD96B");

                entity.HasOne(d => d.IdFuncionarioNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdFuncionario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__ID_FUNCION__1BC821DD");

                entity.HasOne(d => d.IdPlataformaNavigation)
                    .WithMany(p => p.Jogos)
                    .HasForeignKey(d => d.IdPlataforma)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jogo__ID_PLATAFO__1AD3FDA4");
            });

            modelBuilder.Entity<Preferencia>(entity => {
                entity.HasKey(e => new { e.IdCliente, e.IdCategoria })
                    .HasName("PK__Preferen__671E10CADF476741");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Preferencia)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Preferenc__ID_CA__1EA48E88");
            });

            base.OnModelCreating(modelBuilder);

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
