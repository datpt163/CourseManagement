using FPTCourseManagement.Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Slot
{
    public class TypeSlot
    {
        [JsonIgnore]
        private readonly List<Course> courses = new List<Course>();
        public Guid Id { get; private set; }
        public string? Type { get; private set; }
        [JsonIgnore]

        public IReadOnlyCollection<Course> Courses => courses;


    }
}
