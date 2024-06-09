using FPTCourseManagement.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Roles
{
    public class Role
    {
        private readonly List<RolePermission> rolePermissions = new List<RolePermission>();
        private readonly List<User> _users = new List<User>();
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public IReadOnlyCollection<RolePermission> RolePermissions => rolePermissions;
        public IReadOnlyCollection<User> Users => _users;


    }
}
