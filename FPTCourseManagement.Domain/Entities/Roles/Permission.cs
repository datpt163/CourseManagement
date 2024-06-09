using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Roles
{
    public class Permission
    {
        public Guid Id { get; private set; }
        public string? Name { get; private set; }

        private readonly List<RolePermission> rolePermissions = new();
        public IReadOnlyCollection<RolePermission> RolePermissions => rolePermissions;
    }
}
