﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplication1.Domain;

namespace WebApplication1.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.21")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApplication1.Domain.Chapter", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("chapterName")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("level")
                        .HasColumnType("int");

                    b.Property<int>("standardSubjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("standardSubjectId");

                    b.HasIndex("chapterName", "level")
                        .IsUnique()
                        .HasFilter("[chapterName] IS NOT NULL");

                    b.ToTable("Chapters");
                });

            modelBuilder.Entity("WebApplication1.Domain.Standard", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("standardName")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Standards");
                });

            modelBuilder.Entity("WebApplication1.Domain.StandardSubject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("standardId")
                        .HasColumnType("int");

                    b.Property<int>("subjectId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("subjectId");

                    b.HasIndex("standardId", "subjectId")
                        .IsUnique();

                    b.ToTable("StandardSubjects");
                });

            modelBuilder.Entity("WebApplication1.Domain.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("subjectName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Subjects");
                });

            modelBuilder.Entity("WebApplication1.Domain.Chapter", b =>
                {
                    b.HasOne("WebApplication1.Domain.StandardSubject", "standardSubject")
                        .WithMany("chapters")
                        .HasForeignKey("standardSubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("WebApplication1.Domain.StandardSubject", b =>
                {
                    b.HasOne("WebApplication1.Domain.Standard", "standard")
                        .WithMany("standardSubjects")
                        .HasForeignKey("standardId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WebApplication1.Domain.Subject", "subject")
                        .WithMany("standardSubjects")
                        .HasForeignKey("subjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
