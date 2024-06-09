using FPTCourseManagement.Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Slot
{
    public class TypeSlot
    {
        private readonly List<Course> courses = new List<Course>();
        public Guid Id { get; private set; }
        public string? Type { get; private set; }

        public IReadOnlyCollection<Course> Courses => courses;


    }
}
