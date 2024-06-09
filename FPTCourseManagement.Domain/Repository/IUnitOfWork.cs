using FPTCourseManagement.Domain.Entities.Courses;
using FPTCourseManagement.Domain.Entities.Roles;
using FPTCourseManagement.Domain.Entities.Schedules;
using FPTCourseManagement.Domain.Entities.Slot;
using FPTCourseManagement.Domain.Entities.Subjects;
using FPTCourseManagement.Domain.Entities.Users;
using System;
using Microsoft.EntityFrameworkCore.Storage;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Repository
{
    public partial interface IUnitOfWork
    {
        IRepository<Course> Courses { get; }
        IRepository<CourseStudent> CourseStudents { get; }
        IRepository<Permission> Permissions { get; }
        IRepository<RolePermission> RolePermissions { get; }
        IRepository<Role> Roles { get; }
        IRepository<Attendance> Attendances { get; }
        IRepository<Schedule> Schedules { get; }
        IRepository<TimeSlot> TimeSlots { get; }
        IRepository<TypeSlot> TypeSlots { get; }
        IRepository<Subject> Subjects { get; }
        IRepository<Student> Students { get; }
        IRepository<Teacher> Teachers { get; }
        IRepository<User> Users { get; }

        void SaveChanges();
        Task SaveChangesAsync();

        public IDbContextTransaction BeginTransaction();
    }
}
