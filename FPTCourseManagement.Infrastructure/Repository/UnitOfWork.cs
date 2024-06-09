using FPTCourseManagement.Domain.Entities.Courses;
using FPTCourseManagement.Domain.Entities.Roles;
using FPTCourseManagement.Domain.Entities.Schedules;
using FPTCourseManagement.Domain.Entities.Slot;
using FPTCourseManagement.Domain.Entities.Subjects;
using FPTCourseManagement.Domain.Entities.Users;
using FPTCourseManagement.Domain.Repository;
using FPTCourseManagement.Infrastructure.DbContexts;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FPTCourseManagement.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext DbContext;



        public UnitOfWork(AppDbContext dbContext)
        {
            DbContext = dbContext;
        }
        public IRepository<Course> courses;

        public IRepository<CourseStudent> courseStudents;

        public IRepository<Permission> permissions;

        public IRepository<RolePermission> rolePermissions;

        public IRepository<Role> roles;

        public IRepository<Attendance> attendances;

        public IRepository<Schedule> schedules;

        public IRepository<TimeSlot> timeSlots;

        public IRepository<TypeSlot> typeSlots;

        public IRepository<Subject> subjects;
        public IRepository<Student> students;

        public IRepository<Teacher> teachers;

        public IRepository<User> users;


        public IRepository<Course> Courses => courses ?? new Repository<Course>(DbContext);

        public IRepository<CourseStudent> CourseStudents => courseStudents ?? new Repository<CourseStudent>(DbContext);

        public IRepository<Permission> Permissions => permissions ?? new Repository<Permission>(DbContext);

        public IRepository<RolePermission> RolePermissions => rolePermissions ?? new Repository<RolePermission>(DbContext);

        public IRepository<Role> Roles => roles ?? new Repository<Role>(DbContext);

        public IRepository<Attendance> Attendances => attendances ?? new Repository<Attendance>(DbContext);

        public IRepository<Schedule> Schedules => schedules ?? new Repository<Schedule>(DbContext);

        public IRepository<TimeSlot> TimeSlots => timeSlots ?? new Repository<TimeSlot>(DbContext);

        public IRepository<TypeSlot> TypeSlots => typeSlots ?? new Repository<TypeSlot>(DbContext);

        public IRepository<Subject> Subjects => subjects ?? new Repository<Subject>(DbContext);

        public IRepository<Student> Students => students ?? new Repository<Student>(DbContext);


        public IRepository<Teacher> Teachers => teachers ?? new Repository<Teacher>(DbContext);

        public IRepository<User> Users => users ?? new Repository<User>(DbContext);

        public void SaveChanges()
        {
            using (var transactionResult = DbContext.Database.BeginTransaction(System.Data.IsolationLevel.Snapshot))
            {
                try
                {
                    DbContext.SaveChanges();
                    transactionResult.Commit();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SaveChanges: " + ex.GetBaseException());
                    transactionResult.Rollback();
                    throw;
                }
            }

        }

        public async Task SaveChangesAsync()
        {
            using (var transactionResult = await DbContext.Database.BeginTransactionAsync(System.Data.IsolationLevel.Snapshot))
            {
                try
                {
                    await DbContext.SaveChangesAsync();
                    await transactionResult.CommitAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("SaveChangesAsync: " + ex.GetBaseException());
                    await transactionResult.RollbackAsync();
                    throw;
                }
            }
        }

        public IDbContextTransaction BeginTransaction()
        {
            return DbContext.Database.BeginTransaction();
        }


    }
}
