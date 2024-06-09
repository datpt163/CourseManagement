using FPTCourseManagement.Domain.Entities.Courses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Subjects
{
    public class Subject
    {
        private readonly List<Course> courses = new List<Course>(); 
        public Guid Id { get; private set; }
        public string? Name { get; private set; }
        public int NumberOfSlot { get; private set; }
        public IReadOnlyCollection<Course> Courses => courses;

    }
}
