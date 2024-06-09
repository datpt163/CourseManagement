using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPTCourseManagement.Domain.Entities.Users;
using FPTCourseManagement.Domain.Entities.Subjects;
using FPTCourseManagement.Domain.Entities.Slot;
using FPTCourseManagement.Domain.Entities.Schedules;
using FPTCourseManagement.Domain.Entities.Roles;
using FPTCourseManagement.Domain.Entities.Courses;
namespace FPTCourseManagement.Infrastructure.DbContexts

{
    public partial class AppDbContext : DbContext
    {

        public AppDbContext()
        {
        }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<Teacher> Teachers { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<Subject> Subjects { get; set; } = null!;
        public virtual DbSet<TimeSlot> TimeSlots { get; set; } = null!;
        public virtual DbSet<TypeSlot> TypeSlots { get; set; } = null!;
        public virtual DbSet<Attendance> Attendances { get; set; } = null!;
        public virtual DbSet<Schedule> Schedules { get; set; } = null!;
        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseStudent> CourseStudents { get; set; } = null!;
        public virtual DbSet<Permission> Permissions { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<RolePermission> RolePermissions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=localhost;database=CourseManagement;user=root;password=dat123456", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.3.0-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>(e =>
            {
                e.ToTable("permission");
                e.HasKey(e => e.Id);

                e.Property(e => e.Name)
                    .HasMaxLength(25)
                    .HasColumnName("permission_name")
                    .IsFixedLength();
            });

            modelBuilder.Entity<RolePermission>(e =>
            {
                e.ToTable("role_permission");
                e.HasKey(e => e.Id);
                e.HasOne(e => e.Permission)
                 .WithMany(e => e.RolePermissions)
                 .HasForeignKey(e => e.PermissionId);

                e.HasOne(e => e.Role)
                 .WithMany(e => e.RolePermissions)
                 .HasForeignKey(e => e.RoleId);

            });

            modelBuilder.Entity<Role>(e =>
            {
                e.ToTable("role");
                e.HasKey(e => e.Id);
                e.Property(e => e.Name).HasMaxLength(25).IsFixedLength();
            });

            modelBuilder.Entity<User>(e =>
            {
                e.ToTable("user");
                e.HasKey(e => e.Id);
                e.Property(e => e.Email).HasMaxLength(50).IsFixedLength();
                e.Property(e => e.FirstName).HasMaxLength(50).IsFixedLength();
                e.Property(e => e.LastName).HasMaxLength(50).IsFixedLength();
                e.Property(e => e.Password).HasMaxLength(50).IsFixedLength();
                e.HasOne(e => e.Role)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.RoleId);



            });

            modelBuilder.Entity<Student>(e =>
            {
                e.ToTable("student");
                e.HasKey(e => e.AccountId);
                e.Property(e => e.StudentCode).HasMaxLength(25).IsFixedLength();

                e.HasOne(e => e.User)
                .WithMany(e => e.students)
                .HasForeignKey(e => e.AccountId);
            });

            modelBuilder.Entity<Teacher>(e =>
            {
                e.ToTable("teacher");
                e.HasKey(e => e.AccountId);
                e.Property(e => e.TeacherCode).HasMaxLength(25).IsFixedLength();

                e.HasOne(e => e.User)
              .WithMany(e => e.Teachers)
              .HasForeignKey(e => e.AccountId);
            });

            modelBuilder.Entity<CourseStudent>(e =>
            {
                e.ToTable("course_student");
                e.HasKey(e => e.Id);
                e.HasOne(e => e.Course)
                .WithMany(e => e.CourseStudents)
                .HasForeignKey(e => e.CourseId);



                e.HasOne(e => e.Student)
              .WithMany(e => e.CourseStudents)
              .HasForeignKey(e => e.StudentId);

            });

            modelBuilder.Entity<Course>(e =>
            {
                e.ToTable("course");
                e.HasKey(e => e.Id);
                e.HasIndex(e => e.CourseCode).IsUnique();
                e.Property(e => e.CourseCode).HasMaxLength(25).IsFixedLength();

                e.HasOne(e => e.TypeSlot)
                .WithMany(e => e.Courses)
                .HasForeignKey(e => e.TypeSlotId);

                e.HasOne(e => e.Subject)
                .WithMany(e => e.Courses)
                .HasForeignKey(e => e.SubjectId);
            });

            modelBuilder.Entity<Schedule>(e =>
            {
                e.ToTable("schedule");
                e.HasKey(e => e.Id);
                e.HasOne(e => e.Course)
                .WithMany(e => e.Schedules)
                .HasForeignKey(e => e.CourseId);

                e.HasOne(e => e.Teacher)
                .WithMany(e => e.Schedules)
                .HasForeignKey(e => e.TeacherId);

                e.HasOne(e => e.TimeSlot)
                .WithMany(e => e.Schedules)
                .HasForeignKey(e => e.TimeSlotId);
            });

            modelBuilder.Entity<Attendance>(e =>
            {
                e.ToTable("attendance");
                e.HasKey(e => e.Id);

                e.HasOne(e => e.Student)
                .WithMany(e => e.Attendances)
                .HasForeignKey(e => e.StudentId);

                e.HasOne(e => e.Schedule)
               .WithMany(e => e.Attendances)
               .HasForeignKey(e => e.ScheduleId);
            });

            modelBuilder.Entity<TypeSlot>(e =>
            {
                e.ToTable("type_slot");

                e.HasKey(e => e.Id);

                e.Property(e => e.Type).HasMaxLength(25).IsFixedLength();
            });

            modelBuilder.Entity<TimeSlot>(e =>
            {
                e.ToTable("time_slot");

                e.HasKey(e => e.Id);

            });
        }
    }
}
