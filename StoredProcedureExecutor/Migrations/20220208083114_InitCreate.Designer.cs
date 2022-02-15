﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using StoredProcedureExecutor.Data;

#nullable disable

namespace StoredProcedureExecutor.Migrations
{
    [DbContext(typeof(ProceduresDbContext))]
    [Migration("20220208083114_InitCreate")]
    partial class InitCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("StoredProcedureExecutor.Models.Procedure", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Database")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("EmailRecipients")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("EmailSubject")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("LastExecutedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastRefreshedAt")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastSentTemplateAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("OutputReportPath")
                        .HasMaxLength(260)
                        .HasColumnType("nvarchar(260)");

                    b.Property<string>("Schema")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<string>("Server")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.HasKey("Id");

                    b.ToTable("SPExecutorApp_Procedures", "BrightLight");
                });

            modelBuilder.Entity("StoredProcedureExecutor.Models.ProcedureParam", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("nvarchar(128)");

                    b.Property<int>("ProcedureId")
                        .HasColumnType("int");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("ProcedureId");

                    b.ToTable("SPExecutorApp_ProcedureParams", "BrightLight");
                });

            modelBuilder.Entity("StoredProcedureExecutor.Models.Template", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<byte[]>("DataFile")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("FileType")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProcedureId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProcedureId")
                        .IsUnique();

                    b.ToTable("SPExecutorApp_Templates", "BrightLight");
                });

            modelBuilder.Entity("StoredProcedureExecutor.Models.ProcedureParam", b =>
                {
                    b.HasOne("StoredProcedureExecutor.Models.Procedure", "Procedure")
                        .WithMany("Params")
                        .HasForeignKey("ProcedureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Procedure");
                });

            modelBuilder.Entity("StoredProcedureExecutor.Models.Template", b =>
                {
                    b.HasOne("StoredProcedureExecutor.Models.Procedure", "Procedure")
                        .WithOne("Template")
                        .HasForeignKey("StoredProcedureExecutor.Models.Template", "ProcedureId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Procedure");
                });

            modelBuilder.Entity("StoredProcedureExecutor.Models.Procedure", b =>
                {
                    b.Navigation("Params");

                    b.Navigation("Template");
                });
#pragma warning restore 612, 618
        }
    }
}