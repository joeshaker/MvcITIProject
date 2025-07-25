﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MvcITIProject.Models;

#nullable disable

namespace MvcITIProject.Migrations
{
    [DbContext(typeof(LibraryContext))]
    [Migration("20250627210334_setNullOnDelete")]
    partial class setNullOnDelete
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("SQL_Latin1_General_CP1_CI_AS")
                .HasAnnotation("ProductVersion", "9.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BookAuthor", b =>
                {
                    b.Property<int>("BookId")
                        .HasColumnType("int")
                        .HasColumnName("Book_id");

                    b.Property<int>("AuthorId")
                        .HasColumnType("int")
                        .HasColumnName("Author_id");

                    b.HasKey("BookId", "AuthorId");

                    b.HasIndex("AuthorId");

                    b.ToTable("Book_Author", (string)null);
                });

            modelBuilder.Entity("MvcITIProject.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Author", (string)null);
                });

            modelBuilder.Entity("MvcITIProject.Models.Book", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CatId")
                        .HasColumnType("int")
                        .HasColumnName("Cat_id");

                    b.Property<int?>("PublisherId")
                        .HasColumnType("int")
                        .HasColumnName("Publisher_id");

                    b.Property<string>("ShelfCode")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)")
                        .HasColumnName("Shelf_code");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("CatId");

                    b.HasIndex("PublisherId");

                    b.HasIndex("ShelfCode");

                    b.ToTable("Book", (string)null);
                });

            modelBuilder.Entity("MvcITIProject.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CatName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("Cat_name");

                    b.HasKey("Id");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("MvcITIProject.Models.Floor", b =>
                {
                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<DateOnly?>("HiringDate")
                        .HasColumnType("date")
                        .HasColumnName("Hiring_Date");

                    b.Property<int>("NumBlocks")
                        .HasColumnType("int")
                        .HasColumnName("Num_blocks");

                    b.HasKey("Number");

                    b.ToTable("Floor", (string)null);
                });

            modelBuilder.Entity("MvcITIProject.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Publisher", (string)null);
                });

            modelBuilder.Entity("MvcITIProject.Models.Shelf", b =>
                {
                    b.Property<string>("Code")
                        .HasMaxLength(3)
                        .IsUnicode(false)
                        .HasColumnType("varchar(3)");

                    b.Property<int?>("FloorNum")
                        .HasColumnType("int")
                        .HasColumnName("Floor_num");

                    b.HasKey("Code");

                    b.HasIndex("FloorNum");

                    b.ToTable("Shelf", (string)null);
                });

            modelBuilder.Entity("BookAuthor", b =>
                {
                    b.HasOne("MvcITIProject.Models.Author", null)
                        .WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Book_Author_Author");

                    b.HasOne("MvcITIProject.Models.Book", null)
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Book_Author_Book");
                });

            modelBuilder.Entity("MvcITIProject.Models.Book", b =>
                {
                    b.HasOne("MvcITIProject.Models.Category", "Cat")
                        .WithMany("Books")
                        .HasForeignKey("CatId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Book_Category");

                    b.HasOne("MvcITIProject.Models.Publisher", "Publisher")
                        .WithMany("Books")
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Book_Publisher");

                    b.HasOne("MvcITIProject.Models.Shelf", "ShelfCodeNavigation")
                        .WithMany("Books")
                        .HasForeignKey("ShelfCode")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Book_Shelf");

                    b.Navigation("Cat");

                    b.Navigation("Publisher");

                    b.Navigation("ShelfCodeNavigation");
                });

            modelBuilder.Entity("MvcITIProject.Models.Shelf", b =>
                {
                    b.HasOne("MvcITIProject.Models.Floor", "FloorNumNavigation")
                        .WithMany("Shelves")
                        .HasForeignKey("FloorNum")
                        .OnDelete(DeleteBehavior.SetNull)
                        .HasConstraintName("FK_Shelf_Floor");

                    b.Navigation("FloorNumNavigation");
                });

            modelBuilder.Entity("MvcITIProject.Models.Category", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("MvcITIProject.Models.Floor", b =>
                {
                    b.Navigation("Shelves");
                });

            modelBuilder.Entity("MvcITIProject.Models.Publisher", b =>
                {
                    b.Navigation("Books");
                });

            modelBuilder.Entity("MvcITIProject.Models.Shelf", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
