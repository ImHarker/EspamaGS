﻿// <auto-generated />
using System;
using EspamaGS_2._0.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EspamaGS_2._0.Data.Migrations
{
    [DbContext(typeof(EspamaGSContext))]
    [Migration("20230128003425_Reviews2")]
    partial class Reviews2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("EspamaGS_2._0.Models.Administrador", b =>
                {
                    b.Property<string>("IdUtilizador")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ID_UTILIZADOR");

                    b.Property<DateTime>("DataRegisto")
                        .HasColumnType("datetime")
                        .HasColumnName("DATA_REGISTO");

                    b.Property<string>("IdAdmin")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ID_ADMIN");

                    b.HasKey("IdUtilizador")
                        .HasName("PK__Administ__7BC3137CC20F3687");

                    b.HasIndex(new[] { "IdAdmin" }, "IX_Administrador_ID_ADMIN");

                    b.ToTable("Administrador");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Cart", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("IdCliente")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("IdJogo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("IdJogo");

                    b.ToTable("Cart");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("NOME");

                    b.HasKey("Id");

                    b.ToTable("Categoria");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataCompra")
                        .HasColumnType("datetime")
                        .HasColumnName("DATA_COMPRA");

                    b.Property<string>("IdCliente")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ID_CLIENTE");

                    b.Property<int>("IdJogo")
                        .HasColumnType("int")
                        .HasColumnName("ID_JOGO");

                    b.Property<string>("Key")
                        .IsRequired()
                        .HasMaxLength(18)
                        .IsUnicode(false)
                        .HasColumnType("varchar(18)")
                        .HasColumnName("KEY_JOGO");

                    b.Property<decimal>("Preco")
                        .HasColumnType("money")
                        .HasColumnName("PRECO");

                    b.HasKey("Id")
                        .HasName("PK__Compra__6B235F1E45A1618F");

                    b.HasIndex(new[] { "IdJogo" }, "IX_Compra_ID_JOGO");

                    b.ToTable("Compra");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Desenvolvedora", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOME");

                    b.HasKey("Id");

                    b.ToTable("Desenvolvedora");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Funcionario", b =>
                {
                    b.Property<string>("IdUtilizador")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ID_UTILIZADOR");

                    b.Property<DateTime>("DataRegisto")
                        .HasColumnType("datetime")
                        .HasColumnName("DATA_REGISTO");

                    b.Property<string>("IdAdmin")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ID_ADMIN");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasMaxLength(9)
                        .IsUnicode(false)
                        .HasColumnType("varchar(9)")
                        .HasColumnName("TELEFONE");

                    b.HasKey("IdUtilizador")
                        .HasName("PK__Funciona__7BC3137C400E1255");

                    b.HasIndex(new[] { "IdAdmin" }, "IX_Funcionario_ID_ADMIN");

                    b.ToTable("Funcionario");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Jogo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataLancamento")
                        .HasColumnType("date")
                        .HasColumnName("DATA_LANCAMENTO");

                    b.Property<DateTime>("DataRegisto")
                        .HasColumnType("datetime")
                        .HasColumnName("DATA_REGISTO");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .IsUnicode(false)
                        .HasColumnType("varchar(1024)")
                        .HasColumnName("DESCRICAO");

                    b.Property<string>("Foto")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("FOTO");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int")
                        .HasColumnName("ID_CATEGORIA");

                    b.Property<int>("IdDesenvolvedora")
                        .HasColumnType("int")
                        .HasColumnName("ID_DESENVOLVEDORA");

                    b.Property<string>("IdFuncionario")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ID_FUNCIONARIO");

                    b.Property<int>("IdPlataforma")
                        .HasColumnType("int")
                        .HasColumnName("ID_PLATAFORMA");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("NOME");

                    b.Property<decimal>("Preco")
                        .HasColumnType("money")
                        .HasColumnName("PRECO");

                    b.HasKey("Id");

                    b.HasIndex(new[] { "IdCategoria" }, "IX_Jogo_ID_CATEGORIA");

                    b.HasIndex(new[] { "IdDesenvolvedora" }, "IX_Jogo_ID_DESENVOLVEDORA");

                    b.HasIndex(new[] { "IdFuncionario" }, "IX_Jogo_ID_FUNCIONARIO");

                    b.HasIndex(new[] { "IdPlataforma" }, "IX_Jogo_ID_PLATAFORMA");

                    b.ToTable("Jogo");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Plataforma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("NOME");

                    b.HasKey("Id");

                    b.ToTable("Plataforma");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Preferencia", b =>
                {
                    b.Property<string>("IdCliente")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("ID_CLIENTE");

                    b.Property<int>("IdCategoria")
                        .HasColumnType("int")
                        .HasColumnName("ID_CATEGORIA");

                    b.HasKey("IdCliente", "IdCategoria")
                        .HasName("PK__Preferen__671E10CADF476741");

                    b.HasIndex(new[] { "IdCategoria" }, "IX_Preferencia_ID_CATEGORIA");

                    b.ToTable("Preferencia");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Review", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("DataReview")
                        .HasColumnType("datetime2");

                    b.Property<string>("IdCliente")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("IdJogo")
                        .HasColumnType("int");

                    b.Property<int>("Rating")
                        .HasColumnType("int");

                    b.Property<string>("ReviewMessage")
                        .IsRequired()
                        .HasMaxLength(1024)
                        .HasColumnType("nvarchar(1024)");

                    b.HasKey("Id");

                    b.HasIndex("IdJogo");

                    b.ToTable("Reviews");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.UserSettings", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<bool>("EmailNotifications")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("nvarchar(256)");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens", (string)null);
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Administrador", b =>
                {
                    b.HasOne("EspamaGS_2._0.Models.Administrador", "IdAdminNavigation")
                        .WithMany("InverseIdAdminNavigation")
                        .HasForeignKey("IdAdmin")
                        .IsRequired()
                        .HasConstraintName("FK__Administr__ID_AD__1332DBDC");

                    b.Navigation("IdAdminNavigation");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Cart", b =>
                {
                    b.HasOne("EspamaGS_2._0.Models.Jogo", "IdJogoNavigation")
                        .WithMany("Carts")
                        .HasForeignKey("IdJogo")
                        .IsRequired()
                        .HasConstraintName("FK__Cart__IdJogo__160F4887");

                    b.Navigation("IdJogoNavigation");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Compra", b =>
                {
                    b.HasOne("EspamaGS_2._0.Models.Jogo", "IdJogoNavigation")
                        .WithMany("Compras")
                        .HasForeignKey("IdJogo")
                        .IsRequired()
                        .HasConstraintName("FK__Compra__ID_JOGO__2180FB33");

                    b.Navigation("IdJogoNavigation");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Funcionario", b =>
                {
                    b.HasOne("EspamaGS_2._0.Models.Administrador", "IdAdminNavigation")
                        .WithMany("Funcionarios")
                        .HasForeignKey("IdAdmin")
                        .IsRequired()
                        .HasConstraintName("FK__Funcionar__ID_AD__160F4887");

                    b.Navigation("IdAdminNavigation");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Jogo", b =>
                {
                    b.HasOne("EspamaGS_2._0.Models.Categoria", "IdCategoriaNavigation")
                        .WithMany("Jogos")
                        .HasForeignKey("IdCategoria")
                        .IsRequired()
                        .HasConstraintName("FK__Jogo__ID_CATEGOR__18EBB532");

                    b.HasOne("EspamaGS_2._0.Models.Desenvolvedora", "IdDesenvolvedoraNavigation")
                        .WithMany("Jogos")
                        .HasForeignKey("IdDesenvolvedora")
                        .IsRequired()
                        .HasConstraintName("FK__Jogo__ID_DESENVO__19DFD96B");

                    b.HasOne("EspamaGS_2._0.Models.Funcionario", "IdFuncionarioNavigation")
                        .WithMany("Jogos")
                        .HasForeignKey("IdFuncionario")
                        .IsRequired()
                        .HasConstraintName("FK__Jogo__ID_FUNCION__1BC821DD");

                    b.HasOne("EspamaGS_2._0.Models.Plataforma", "IdPlataformaNavigation")
                        .WithMany("Jogos")
                        .HasForeignKey("IdPlataforma")
                        .IsRequired()
                        .HasConstraintName("FK__Jogo__ID_PLATAFO__1AD3FDA4");

                    b.Navigation("IdCategoriaNavigation");

                    b.Navigation("IdDesenvolvedoraNavigation");

                    b.Navigation("IdFuncionarioNavigation");

                    b.Navigation("IdPlataformaNavigation");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Preferencia", b =>
                {
                    b.HasOne("EspamaGS_2._0.Models.Categoria", "IdCategoriaNavigation")
                        .WithMany("Preferencia")
                        .HasForeignKey("IdCategoria")
                        .IsRequired()
                        .HasConstraintName("FK__Preferenc__ID_CA__1EA48E88");

                    b.Navigation("IdCategoriaNavigation");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Review", b =>
                {
                    b.HasOne("EspamaGS_2._0.Models.Jogo", "IdJogoNavigation")
                        .WithMany()
                        .HasForeignKey("IdJogo")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("IdJogoNavigation");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Administrador", b =>
                {
                    b.Navigation("Funcionarios");

                    b.Navigation("InverseIdAdminNavigation");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Categoria", b =>
                {
                    b.Navigation("Jogos");

                    b.Navigation("Preferencia");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Desenvolvedora", b =>
                {
                    b.Navigation("Jogos");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Funcionario", b =>
                {
                    b.Navigation("Jogos");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Jogo", b =>
                {
                    b.Navigation("Carts");

                    b.Navigation("Compras");
                });

            modelBuilder.Entity("EspamaGS_2._0.Models.Plataforma", b =>
                {
                    b.Navigation("Jogos");
                });
#pragma warning restore 612, 618
        }
    }
}