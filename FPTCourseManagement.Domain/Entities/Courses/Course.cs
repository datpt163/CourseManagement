using FPTCourseManagement.Domain.Entities.Schedules;
using FPTCourseManagement.Domain.Entities.Slot;
using FPTCourseManagement.Domain.Entities.Subjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Courses
{
    public class Course
    {
        [JsonIgnore]
        private readonly List<CourseStudent> courseStudents = new List<CourseStudent>();
        [JsonIgnore]
        private readonly List<Schedule> schedules = new List<Schedule>();

        public Course(Guid id, string code, DateTime startDate, Guid typeslotId, Guid subjectId) {
            Id = id;
            CourseCode = code;
            StartDate = startDate;
            TypeSlotId = typeslotId;
            SubjectId = subjectId;

        }

        public Course() { }

        public Guid Id { get; private set; }
        public string? CourseCode { get; private set; }
        public DateTime StartDate { get; private set; }
        public Guid TypeSlotId { get; private set; }
        public Guid SubjectId { get; private set; }
        [JsonIgnore]
        public virtual Subject? Subject { get; private set; }
        [JsonIgnore]
        public virtual TypeSlot? TypeSlot { get; private set; }
        [JsonIgnore]
        public IReadOnlyCollection<CourseStudent> CourseStudents => courseStudents;
        [JsonIgnore]
        public IReadOnlyCollection<Schedule> Schedules => schedules;

        public static Course Create(Guid id, string courseCode, DateTime start, Guid typeSlotId, Guid subjectId  )
        {
            return new Course(id, courseCode, start, typeSlotId, subjectId);
        }

        public static (bool checkStartDate1, bool checkGroup1) CheckDayStartDate(DateTime date, string ts)
        {
            bool CheckStartDate = false;
            bool checkGroup = false;
            if (ts == "2")
            {
                if (date.DayOfWeek == DayOfWeek.Monday)
                    CheckStartDate = true;
            }
            else if (ts == "3")
            {
                if (date.DayOfWeek == DayOfWeek.Tuesday)
                    CheckStartDate = true;
            }
            else if (ts == "4")
            {
                if (date.DayOfWeek == DayOfWeek.Wednesday)
                    CheckStartDate = true;
            }
            else if (ts == "5")
            {
                if (date.DayOfWeek == DayOfWeek.Thursday)
                {
                    CheckStartDate = true;
                    checkGroup = true;
                }
            }
            else if (ts == "6")
            {
                if (date.DayOfWeek == DayOfWeek.Friday)
                {
                    CheckStartDate = true;
                    checkGroup = true;
                }
            }
            return (CheckStartDate, checkGroup);
        }
    }
}
