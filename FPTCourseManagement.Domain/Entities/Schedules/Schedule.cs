using FPTCourseManagement.Domain.Entities.Courses;
using FPTCourseManagement.Domain.Entities.Slot;
using FPTCourseManagement.Domain.Entities.Subjects;
using FPTCourseManagement.Domain.Entities.Users;
using FPTCourseManagement.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FPTCourseManagement.Domain.Entities.Schedules
{
    public class Schedule
    {
        [JsonIgnore]
        private readonly List<Attendance> attendances = new List<Attendance>();
        public Guid Id { get; private set; }

        public Guid TimeSlotId { get; private set; }
        public Guid TeacherId { get; private set; }
        public Guid CourseId { get; private set; }

        public DateTime DateOfLessson { get; private set; }

        public Schedule(Guid id, Guid timeslotId, Guid teacherId, Guid courseId, DateTime dateOfLessson)
        {
            Id = id;
            TimeSlotId = timeslotId;
            CourseId = courseId;
            TeacherId = teacherId;
            DateOfLessson = dateOfLessson;
        }

        public Schedule()
        {

        }
        [JsonIgnore]
        public IReadOnlyCollection<Attendance> Attendances => attendances;
        [JsonIgnore]
        public virtual Teacher? Teacher { get; private set; }
        [JsonIgnore]
        public virtual Course? Course { get; private set; }
        [JsonIgnore]
        public virtual TimeSlot? TimeSlot { get; private set; }


        public static Schedule Create(Guid id, Guid timeslotId, Guid teacherId,  Guid courseId, DateTime dateOfLessson)
        {
            return new Schedule(id, timeslotId, teacherId, courseId, dateOfLessson);
        }

    }
}
