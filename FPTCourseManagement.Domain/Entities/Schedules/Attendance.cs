using FPTCourseManagement.Domain.Entities.Users;
using FPTCourseManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Schedules
{
    public class Attendance
    {

        public Guid Id { get;  set; }
        public Guid StudentId { get;  set; }
        public Guid ScheduleId { get;  set; }
        public AttendanceStatus Status { get;  set; }
        public virtual Student? Student { get;  set; }
        [JsonIgnore]
        public virtual Schedule? Schedule { get;  set; }
    }
}
