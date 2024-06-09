using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Roles
{
    public class RolePermission
    {
        public Guid Id { get; private set; }
        public Guid PermissionId { get; private set; }
        public Guid RoleId { get; private set; }

        public virtual Role? Role { get; private set; }
        public virtual Permission? Permission { get; private set; }

    }
}
