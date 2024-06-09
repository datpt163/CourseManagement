using FPTCourseManagement.Domain.Entities.Roles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Users
{
    public class User
    {
        public List<Student> students = new List<Student>();
        public List<Teacher> teachers = new List<Teacher>();
        public Guid Id { get; private set; }
        public string? Email { get; private set; }
        public string? Password { get; private set; }
        public string? FirstName { get; private set; }
        public string? LastName { get; private set; }
        public Guid RoleId { get; private set; }   
        
        public IReadOnlyCollection<Student>  Students => students;
        public virtual Role? Role { get; private set; }
        public IReadOnlyCollection<Teacher> Teachers => teachers;
    }
}
