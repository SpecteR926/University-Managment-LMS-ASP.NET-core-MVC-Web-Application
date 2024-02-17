using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace dbfirstapp.Models;

public partial class LmsDatabaseContext : DbContext
{
    public LmsDatabaseContext()
    {
    }

    public LmsDatabaseContext(DbContextOptions<LmsDatabaseContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseReg> CourseRegs { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Exam> Exams { get; set; }

    public virtual DbSet<Schedule> Schedules { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Teacher> Teachers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.AId).HasName("PK__admin__71AD61D9A6676F2D");

            entity.ToTable("admin");

            entity.Property(e => e.AId)
                .ValueGeneratedNever()
                .HasColumnName("A_id");
            entity.Property(e => e.APassword)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("A_password");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.CId).HasName("PK__Courses__213EE774772D5404");

            entity.HasIndex(e => e.CName, "UQ__Courses__3006C73626A6B1CE").IsUnique();

            entity.Property(e => e.CId).HasColumnName("c_id");
            entity.Property(e => e.CName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("C_Name");
            entity.Property(e => e.DeptmentId).HasColumnName("Deptment_id");

            entity.HasOne(d => d.Deptment).WithMany(p => p.Courses)
                .HasForeignKey(d => d.DeptmentId)
                .HasConstraintName("FK__Courses__Deptmen__5BE2A6F2");
        });

        modelBuilder.Entity<CourseReg>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("course_reg");

            entity.Property(e => e.NoOfRegsStds).HasColumnName("no_of_regs_stds");
            entity.Property(e => e.RDept).HasColumnName("r_dept");
            entity.Property(e => e.RId).HasColumnName("r_id");
            entity.Property(e => e.RName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("r_name");
            entity.Property(e => e.RTeacher).HasColumnName("r_teacher");
            entity.Property(e => e.RTeacherName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("r_teacher_name");

            entity.HasOne(d => d.RDeptNavigation).WithMany()
                .HasForeignKey(d => d.RDept)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course_re__r_dep__0D7A0286");

            entity.HasOne(d => d.RIdNavigation).WithMany()
                .HasForeignKey(d => d.RId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__course_reg__r_id__0B91BA14");

            entity.HasOne(d => d.RNameNavigation).WithMany()
                .HasPrincipalKey(p => p.CName)
                .HasForeignKey(d => d.RName)
                .HasConstraintName("FK__course_re__r_nam__0C85DE4D");

            entity.HasOne(d => d.RTeacherNavigation).WithMany()
                .HasForeignKey(d => d.RTeacher)
                .HasConstraintName("FK__course_re__r_tea__0E6E26BF");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DId).HasName("PK__Departme__D95F582BFCC824F2");

            entity.ToTable("Department");

            entity.HasIndex(e => e.DName, "UQ__Departme__024E29D685276768").IsUnique();

            entity.Property(e => e.DId).HasColumnName("d_id");
            entity.Property(e => e.DName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("d_Name");
        });

        modelBuilder.Entity<Exam>(entity =>
        {
            entity.HasKey(e => e.EId).HasName("PK__Exam__D7E94DACC4839A5B");

            entity.ToTable("Exam");

            entity.Property(e => e.EId).HasColumnName("E_id");
            entity.Property(e => e.CourseId).HasColumnName("Course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Course_name");
            entity.Property(e => e.EDate).HasColumnName("E_date");
            entity.Property(e => e.ETime).HasColumnName("E_time");
            entity.Property(e => e.RNumber)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("R_number");

            entity.HasOne(d => d.Course).WithMany(p => p.ExamCourses)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__Exam__Course_id__02FC7413");

            entity.HasOne(d => d.CourseNameNavigation).WithMany(p => p.ExamCourseNameNavigations)
                .HasPrincipalKey(p => p.CName)
                .HasForeignKey(d => d.CourseName)
                .HasConstraintName("FK__Exam__Course_nam__03F0984C");
        });

        modelBuilder.Entity<Schedule>(entity =>
        {
            entity.HasKey(e => e.EId).HasName("PK__schedule__D7E94DAC21FCD7FA");

            entity.ToTable("schedule");

            entity.Property(e => e.EId).HasColumnName("E_id");
            entity.Property(e => e.CourseId).HasColumnName("Course_id");
            entity.Property(e => e.CourseName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("Course_name");
            entity.Property(e => e.DeptmentId).HasColumnName("Deptment_id");
            entity.Property(e => e.EDate).HasColumnName("E_date");
            entity.Property(e => e.ETime).HasColumnName("E_time");
            entity.Property(e => e.RNumber)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("R_number");
            entity.Property(e => e.TeacherId).HasColumnName("Teacher_id");

            entity.HasOne(d => d.Course).WithMany(p => p.ScheduleCourses)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK__schedule__Course__06CD04F7");

            entity.HasOne(d => d.CourseNameNavigation).WithMany(p => p.ScheduleCourseNameNavigations)
                .HasPrincipalKey(p => p.CName)
                .HasForeignKey(d => d.CourseName)
                .HasConstraintName("FK__schedule__Course__09A971A2");

            entity.HasOne(d => d.Deptment).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.DeptmentId)
                .HasConstraintName("FK__schedule__Deptme__07C12930");

            entity.HasOne(d => d.Teacher).WithMany(p => p.Schedules)
                .HasForeignKey(d => d.TeacherId)
                .HasConstraintName("FK__schedule__Teache__08B54D69");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.SId).HasName("PK__Student__A3DCCCA53FD2BE1B");

            entity.ToTable("Student");

            entity.HasIndex(e => e.Email, "UQ__Student__A9D105341B69794D").IsUnique();

            entity.Property(e => e.SId).HasColumnName("S_id");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber).HasColumnName("phone number");
            entity.Property(e => e.SName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("S_Name");
            entity.Property(e => e.SPassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("S_Password");
        });

        modelBuilder.Entity<Teacher>(entity =>
        {
            entity.HasKey(e => e.TId).HasName("PK__Teacher__E579775F5A5C513F");

            entity.ToTable("Teacher");

            entity.HasIndex(e => e.Email, "UQ__Teacher__A9D10534F7A8D3C7").IsUnique();

            entity.Property(e => e.TId).HasColumnName("t_id");
            entity.Property(e => e.Dob).HasColumnName("DOB");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber).HasColumnName("phone number");
            entity.Property(e => e.TName)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("t_Name");
            entity.Property(e => e.TPassword)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("t_Password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
