using FPTCourseManagement.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Courses
{
    public class CourseStudent
    {
        public Guid Id { get;  set; }
        public Guid StudentId { get;  set; }
        public Guid CourseId { get;  set; }
        [JsonIgnore]
        public virtual Student? Student { get;  set; }
        [JsonIgnore]
        public virtual Course? Course { get;  set; }
    }
}
