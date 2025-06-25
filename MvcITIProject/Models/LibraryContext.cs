using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MvcITIProject.Models;

public partial class LibraryContext : DbContext
{
    public LibraryContext()
    {
    }

    public LibraryContext(DbContextOptions<LibraryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Borrowing> Borrowings { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Floor> Floors { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<Shelf> Shelves { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserPhone> UserPhones { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=Library;Integrated Security=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseCollation("SQL_Latin1_General_CP1_CI_AS");

        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.CatId).HasColumnName("Cat_id");
            entity.Property(e => e.PublisherId).HasColumnName("Publisher_id");
            entity.Property(e => e.ShelfCode)
                .HasMaxLength(3)
                .IsUnicode(false)
                .HasColumnName("Shelf_code");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasOne(d => d.Cat).WithMany(p => p.Books)
                .HasForeignKey(d => d.CatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Category");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Publisher");

            entity.HasOne(d => d.ShelfCodeNavigation).WithMany(p => p.Books)
                .HasForeignKey(d => d.ShelfCode)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Shelf");

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Book_Author_Author"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_Book_Author_Book"),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId");
                        j.ToTable("Book_Author");
                        j.IndexerProperty<int>("BookId").HasColumnName("Book_id");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("Author_id");
                    });
        });

        modelBuilder.Entity<Borrowing>(entity =>
        {
            entity.HasKey(e => new { e.BookId, e.BorrowDate });

            entity.ToTable("Borrowing");

            entity.Property(e => e.BookId).HasColumnName("Book_id");
            entity.Property(e => e.BorrowDate).HasColumnName("Borrow_date");
            entity.Property(e => e.DueDate).HasColumnName("Due_date");
            entity.Property(e => e.EmpId).HasColumnName("Emp_id");
            entity.Property(e => e.UserSsn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_ssn");

            entity.HasOne(d => d.Book).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Borrowing_Book");

            entity.HasOne(d => d.Emp).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Borrowing_Employee");

            entity.HasOne(d => d.UserSsnNavigation).WithMany(p => p.Borrowings)
                .HasForeignKey(d => d.UserSsn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Borrowing_User");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.CatName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Cat_name");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.ToTable("Employee");

            entity.Property(e => e.Address)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FloorNo).HasColumnName("Floor_no");
            entity.Property(e => e.Fname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Lname)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.SuperId).HasColumnName("Super_id");

            entity.HasOne(d => d.FloorNoNavigation).WithMany(p => p.Employees)
                .HasForeignKey(d => d.FloorNo)
                .HasConstraintName("FK_Employee_Floor");

            entity.HasOne(d => d.Super).WithMany(p => p.InverseSuper)
                .HasForeignKey(d => d.SuperId)
                .HasConstraintName("FK_Employee_Employee");
        });

        modelBuilder.Entity<Floor>(entity =>
        {
            entity.HasKey(e => e.Number);

            entity.ToTable("Floor");

            entity.Property(e => e.Number).ValueGeneratedNever();
            entity.Property(e => e.HiringDate).HasColumnName("Hiring_Date");
            entity.Property(e => e.MgId).HasColumnName("MG_ID");
            entity.Property(e => e.NumBlocks).HasColumnName("Num_blocks");

            entity.HasOne(d => d.Mg).WithMany(p => p.Floors)
                .HasForeignKey(d => d.MgId)
                .HasConstraintName("FK_Floor_Employee");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.ToTable("Publisher");

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Shelf>(entity =>
        {
            entity.HasKey(e => e.Code);

            entity.ToTable("Shelf");

            entity.Property(e => e.Code)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.FloorNum).HasColumnName("Floor_num");

            entity.HasOne(d => d.FloorNumNavigation).WithMany(p => p.Shelves)
                .HasForeignKey(d => d.FloorNum)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Shelf_Floor");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Ssn).HasName("PK_User");

            entity.Property(e => e.Ssn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SSN");
            entity.Property(e => e.EmpId).HasColumnName("Emp_id");
            entity.Property(e => e.UserEmail)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_Email");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("User_Name");

            entity.HasOne(d => d.Emp).WithMany(p => p.Users)
                .HasForeignKey(d => d.EmpId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Employee");
        });

        modelBuilder.Entity<UserPhone>(entity =>
        {
            entity.HasKey(e => new { e.UserSsn, e.PhoneNum });

            entity.ToTable("User_phones");

            entity.Property(e => e.UserSsn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("User_ssn");
            entity.Property(e => e.PhoneNum)
                .HasMaxLength(11)
                .IsUnicode(false)
                .HasColumnName("Phone_num");

            entity.HasOne(d => d.UserSsnNavigation).WithMany(p => p.UserPhones)
                .HasForeignKey(d => d.UserSsn)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_phones_User");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
