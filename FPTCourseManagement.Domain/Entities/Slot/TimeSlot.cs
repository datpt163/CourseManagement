using FPTCourseManagement.Domain.Entities.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Slot
{
    public class TimeSlot
    {
        private readonly List<Schedule> schedules = new List<Schedule>();
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public DateTime Start { get; private set; }
        public DateTime End { get; private set; }
        public IReadOnlyCollection<Schedule> Schedules => schedules;
    }
}
