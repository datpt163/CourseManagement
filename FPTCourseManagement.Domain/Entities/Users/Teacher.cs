using FPTCourseManagement.Domain.Entities.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Users
{
    public class Teacher
    {
        private readonly List<Schedule> schedule = new List<Schedule>();
        public Guid AccountId { get; private set; }
        public string? TeacherCode { get; private set; }
        public string? Phone { get; private set; }
        [JsonIgnore]
        public virtual User? User { get; private set; }
        [JsonIgnore]

        public IReadOnlyCollection<Schedule> Schedules => schedule;

    }
}
