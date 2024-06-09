using FPTCourseManagement.Domain.Entities.Courses;
using FPTCourseManagement.Domain.Entities.Schedules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Users
{
    public class Student
    {
        [JsonIgnore]
        private readonly List<CourseStudent> courseStudents = new List<CourseStudent>();
        [JsonIgnore]
        private readonly List<Attendance> attendances = new List<Attendance>();
        public Guid AccountId { get; private set; }
        public string StudentCode { get; private set; }
        [JsonIgnore]
        public virtual User? User { get; private set; }
        [JsonIgnore]
        public IReadOnlyCollection<CourseStudent> CourseStudents => courseStudents;
        [JsonIgnore]
        public IReadOnlyCollection<Attendance> Attendances => attendances;
    }
}
