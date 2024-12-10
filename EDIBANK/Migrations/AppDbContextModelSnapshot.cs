﻿// <auto-generated />
using System;
using EDIBANK.Conf_Db_With_Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EDIBANK.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("Latin1_General_100_CI_AI_SC_UTF8")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EDIBANK.Models.Monitor.Auditoria", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("id_auditoria")
                        .HasColumnOrder(0);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<string>("Comentario")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("comentarios")
                        .HasColumnOrder(6);

                    b.Property<DateTime>("Fecha")
                        .HasColumnType("datetime2")
                        .HasColumnName("fec_auditoria")
                        .HasColumnOrder(1);

                    b.Property<string>("IPRemota")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ip_remota")
                        .HasColumnOrder(3);

                    b.Property<string>("Modulo")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("modulo")
                        .HasColumnOrder(4);

                    b.Property<string>("Operacion")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("operacion")
                        .HasColumnOrder(5);

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("login_usuario")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.ToTable("auditoria");
                });

            modelBuilder.Entity("EDIBANK.Models.Monitor.EDI", b =>
                {
                    b.Property<string>("Id")
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("id_edi")
                        .HasColumnOrder(0);

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("des_edi")
                        .HasColumnOrder(1);

                    b.HasKey("Id");

                    b.ToTable("edi");
                });

            modelBuilder.Entity("EDIBANK.Models.Monitor.HistoricoIntercambio", b =>
                {
                    b.Property<string>("Id")
                        .IsUnicode(false)
                        .HasColumnType("char(17)")
                        .HasColumnName("id_tracking")
                        .HasColumnOrder(0)
                        .HasComment("YYYYMMDDhhmmsssss");

                    b.Property<string>("ArchivoIntercambio")
                        .IsRequired()
                        .HasColumnType("varchar(59)")
                        .HasColumnName("int_archivo")
                        .HasColumnOrder(9);

                    b.Property<string>("ArchivoOriginal")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ori_archivo")
                        .HasColumnOrder(8);

                    b.Property<DateTime>("Cargado")
                        .HasColumnType("datetime2")
                        .HasColumnName("fec_carga")
                        .HasColumnOrder(1);

                    b.Property<DateTime?>("Descargado")
                        .HasColumnType("datetime2")
                        .HasColumnName("fec_descarga")
                        .HasColumnOrder(2);

                    b.Property<string>("EmisorId")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("edi_envia")
                        .HasColumnOrder(5);

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nro_intercambio")
                        .HasColumnOrder(3);

                    b.Property<string>("ReceptorId")
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("edi_recibe")
                        .HasColumnOrder(6);

                    b.Property<string>("RutaArchivo")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("ruta_archivo")
                        .HasColumnOrder(10);

                    b.Property<long>("Tamano")
                        .HasColumnType("bigint")
                        .HasColumnName("tamano")
                        .HasColumnOrder(11);

                    b.Property<string>("TipoDocumento")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("tip_documento")
                        .HasColumnOrder(7);

                    b.Property<byte>("TipoIntercambio")
                        .HasColumnType("tinyint")
                        .HasColumnName("tip_intercambio")
                        .HasColumnOrder(4);

                    b.HasKey("Id");

                    b.HasIndex("Cargado");

                    b.HasIndex("EmisorId");

                    b.HasIndex("ReceptorId");

                    b.ToTable("hist_intercambios", t =>
                        {
                            t.HasCheckConstraint("CK_tip_intercambio", "[tip_intercambio] IN (0, 1, 2)");
                        });
                });

            modelBuilder.Entity("EDIBANK.Models.Monitor.Intercambio", b =>
                {
                    b.Property<string>("Id")
                        .IsUnicode(false)
                        .HasColumnType("char(17)")
                        .HasColumnName("id_tracking")
                        .HasColumnOrder(0)
                        .HasComment("YYYYMMDDhhmmsssss");

                    b.Property<string>("ArchivoIntercambio")
                        .IsRequired()
                        .HasColumnType("varchar(59)")
                        .HasColumnName("int_archivo")
                        .HasColumnOrder(9);

                    b.Property<string>("ArchivoOriginal")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("ori_archivo")
                        .HasColumnOrder(8);

                    b.Property<DateTime>("Cargado")
                        .HasColumnType("datetime2")
                        .HasColumnName("fec_carga")
                        .HasColumnOrder(1);

                    b.Property<DateTime?>("Descargado")
                        .HasColumnType("datetime2")
                        .HasColumnName("fec_descarga")
                        .HasColumnOrder(2);

                    b.Property<string>("EmisorId")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("edi_envia")
                        .HasColumnOrder(5);

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasColumnName("nro_intercambio")
                        .HasColumnOrder(3);

                    b.Property<string>("ReceptorId")
                        .IsUnicode(false)
                        .HasColumnType("varchar(15)")
                        .HasColumnName("edi_recibe")
                        .HasColumnOrder(6);

                    b.Property<string>("RutaArchivo")
                        .IsRequired()
                        .HasColumnType("varchar(200)")
                        .HasColumnName("ruta_archivo")
                        .HasColumnOrder(10);

                    b.Property<byte>("Status")
                        .HasColumnType("tinyint")
                        .HasColumnName("status")
                        .HasColumnOrder(12);

                    b.Property<long>("Tamano")
                        .HasColumnType("bigint")
                        .HasColumnName("tamano")
                        .HasColumnOrder(11);

                    b.Property<string>("TipoDocumento")
                        .HasColumnType("varchar(50)")
                        .HasColumnName("tip_documento")
                        .HasColumnOrder(7);

                    b.Property<byte>("TipoIntercambio")
                        .HasColumnType("tinyint")
                        .HasColumnName("tip_intercambio")
                        .HasColumnOrder(4);

                    b.HasKey("Id");

                    b.HasIndex("Cargado");

                    b.HasIndex("EmisorId");

                    b.HasIndex("ReceptorId");

                    b.ToTable("intercambios", t =>
                        {
                            t.HasCheckConstraint("CK_status", "[status] IN (0, 1, 2)");

                            t.HasCheckConstraint("CK_tip_intercambio", "[tip_intercambio] IN (0, 1, 2)")
                                .HasName("CK_tip_intercambio1");
                        });
                });

            modelBuilder.Entity("EDIBANK.Models.Users_EdiWeb.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DescripUser")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EDIId")
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

                    b.HasData(
                        new
                        {
                            Id = "f863b756-0d6e-4649-bc9a-0b5fc3ad9c1a",
                            Name = "Admin",
                            NormalizedName = "Admin"
                        },
                        new
                        {
                            Id = "f007f8e5-f114-4520-b016-010f3e3ebcfe",
                            Name = "User",
                            NormalizedName = "User"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

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

            modelBuilder.Entity("EDIBANK.Models.Monitor.HistoricoIntercambio", b =>
                {
                    b.HasOne("EDIBANK.Models.Monitor.EDI", "Emisor")
                        .WithMany("HistoricoEmisores")
                        .HasForeignKey("EmisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EDIBANK.Models.Monitor.EDI", "Receptor")
                        .WithMany("HistoricoReceptores")
                        .HasForeignKey("ReceptorId");

                    b.Navigation("Emisor");

                    b.Navigation("Receptor");
                });

            modelBuilder.Entity("EDIBANK.Models.Monitor.Intercambio", b =>
                {
                    b.HasOne("EDIBANK.Models.Monitor.EDI", "Emisor")
                        .WithMany("Emisores")
                        .HasForeignKey("EmisorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EDIBANK.Models.Monitor.EDI", "Receptor")
                        .WithMany("Receptores")
                        .HasForeignKey("ReceptorId");

                    b.Navigation("Emisor");

                    b.Navigation("Receptor");
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
                    b.HasOne("EDIBANK.Models.Users_EdiWeb.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("EDIBANK.Models.Users_EdiWeb.ApplicationUser", null)
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

                    b.HasOne("EDIBANK.Models.Users_EdiWeb.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("EDIBANK.Models.Users_EdiWeb.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("EDIBANK.Models.Monitor.EDI", b =>
                {
                    b.Navigation("Emisores");

                    b.Navigation("HistoricoEmisores");

                    b.Navigation("HistoricoReceptores");

                    b.Navigation("Receptores");
                });
#pragma warning restore 612, 618
        }
    }
}
