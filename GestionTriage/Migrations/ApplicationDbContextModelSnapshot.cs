﻿// <auto-generated />
using System;
using GestionTriage.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace GestionTriage.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GestionTriage.Models.Hospital", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Hospitales");
                });

            modelBuilder.Entity("GestionTriage.Models.NivelTriage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("TiempoMaximoAtencion")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("NivelesTriage");
                });

            modelBuilder.Entity("GestionTriage.Models.Paciente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Apellidos")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("DUI")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaNacimiento")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaRegistro")
                        .HasColumnType("datetime2");

                    b.Property<string>("Genero")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("GestionTriage.Models.RegistroTriage", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CodigoAtencion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EstadoAtendido")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("FechaHoraAtencion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("FechaHoraFinalizacion")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaHoraIngreso")
                        .HasColumnType("datetime2");

                    b.Property<int>("HospitalId")
                        .HasColumnType("int");

                    b.Property<int>("NivelTriageId")
                        .HasColumnType("int");

                    b.Property<string>("ObservacionesAtencion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ObservacionesIngreso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("HospitalId");

                    b.HasIndex("NivelTriageId");

                    b.HasIndex("PacienteId");

                    b.ToTable("RegistrosTriage");
                });

            modelBuilder.Entity("GestionTriage.Models.ReglaAsignacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("NivelTriageId")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Prioridad")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("NivelTriageId");

                    b.ToTable("ReglasAsignacion");
                });

            modelBuilder.Entity("GestionTriage.Models.RegistroTriage", b =>
                {
                    b.HasOne("GestionTriage.Models.Hospital", "Hospital")
                        .WithMany("RegistrosTriage")
                        .HasForeignKey("HospitalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionTriage.Models.NivelTriage", "NivelTriage")
                        .WithMany("RegistrosTriage")
                        .HasForeignKey("NivelTriageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("GestionTriage.Models.Paciente", "Paciente")
                        .WithMany("RegistrosTriage")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Hospital");

                    b.Navigation("NivelTriage");

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("GestionTriage.Models.ReglaAsignacion", b =>
                {
                    b.HasOne("GestionTriage.Models.NivelTriage", "NivelTriage")
                        .WithMany("ReglasAsignacion")
                        .HasForeignKey("NivelTriageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("NivelTriage");
                });

            modelBuilder.Entity("GestionTriage.Models.Hospital", b =>
                {
                    b.Navigation("RegistrosTriage");
                });

            modelBuilder.Entity("GestionTriage.Models.NivelTriage", b =>
                {
                    b.Navigation("RegistrosTriage");

                    b.Navigation("ReglasAsignacion");
                });

            modelBuilder.Entity("GestionTriage.Models.Paciente", b =>
                {
                    b.Navigation("RegistrosTriage");
                });
#pragma warning restore 612, 618
        }
    }
}
