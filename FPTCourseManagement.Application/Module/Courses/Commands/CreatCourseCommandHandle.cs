using System;
using FPTCourseManagement.Domain.Repository;
using FPTCourseManagement.Application.Abtractions.Messaging;
using MediatR;
using FPTCourseManagement.Domain.Entities.Courses;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
using FPTCourseManagement.Domain.Entities.Schedules;

namespace FPTCourseManagement.Application.Module.Courses.Commands
{
    public class CreatCourseCommandHandle : IRequestHandler<CreateCourseCommand, Result>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreatCourseCommandHandle(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(CreateCourseCommand request, CancellationToken token)
        {
            #region check Validate
            var c = _unitOfWork.Courses.FindByCondition(s => s.CourseCode == request.CourseCode).FirstOrDefault();
            if (c != null)
                return new Result(false, "Duplicate course code", new Course());

            if (request.StartDate <= DateTime.Now)
                return new Result(false, "Start date is belong the pass", new Course());

            var typeSlot = _unitOfWork.TypeSlots.FindByCondition(s => s.Id == request.TypeSlotId).FirstOrDefault();
            if (typeSlot == null)
                return new Result(false, "Type Slot does not exist", new Course());

            var teacher = _unitOfWork.Teachers.FindByCondition(s => s.AccountId == request.TeacherId).FirstOrDefault();
            if (teacher == null)
                return new Result(false, "Teacher does not exist", new Course());


            var subject = _unitOfWork.Subjects.FindByCondition(s => s.Id == request.SubjectId).FirstOrDefault();
            if (subject == null)
                return new Result(false, "Subject does not exist", new Course());


            var ts = typeSlot.Type[1] + "";

            (bool CheckStartDate, bool checkGroup) = Course.CheckDayStartDate(request.StartDate, ts);
            if (!CheckStartDate)
                return new Result(false, "Day of Start Date not match TypeSlot", new Course());
            #endregion


            var course = Course.Create(Guid.NewGuid(), request.CourseCode, request.StartDate, request.TypeSlotId, request.SubjectId);

            List<Schedule> list = new List<Schedule>();

            int numberOfSlot = subject.NumberOfSlot;
            DateTime flagDate = request.StartDate;
            int count = 0;
            if (numberOfSlot % 2 == 0)
                count = numberOfSlot / 2;
            else
                count = (numberOfSlot + 1) / 2;

            for (int i = 0; i < count; i++)
            {
                if (typeSlot.Type[0] + "" == "A")
                {
                    var ts2 = _unitOfWork.TimeSlots.FindByCondition(s => s.Name == "slot1").FirstOrDefault();
                    var sc = Schedule.Create(Guid.NewGuid(), ts2.Id, request.TeacherId, course.Id, flagDate);
                    list.Add(sc);
                }
                else
                {
                    var ts2 = _unitOfWork.TimeSlots.FindByCondition(s => s.Name == "slot3").FirstOrDefault();
                    var sc = Schedule.Create(Guid.NewGuid(), ts2.Id, request.TeacherId, course.Id, flagDate);
                    list.Add(sc);
                }

                if (i == count - 1 && numberOfSlot % 2 != 0)
                    break;
                else
                {
                    if (checkGroup)
                    {
                        if (typeSlot.Type[0] + "" == "A")
                        {
                            var ts2 = _unitOfWork.TimeSlots.FindByCondition(s => s.Name == "slot2").FirstOrDefault();
                            var sc = Schedule.Create(Guid.NewGuid(), ts2.Id, request.TeacherId, course.Id, flagDate.AddDays(4));
                            list.Add(sc);
                        }
                        else
                        {
                            var ts2 = _unitOfWork.TimeSlots.FindByCondition(s => s.Name == "slot4").FirstOrDefault();
                            var sc = Schedule.Create(Guid.NewGuid(), ts2.Id, request.TeacherId, course.Id, flagDate.AddDays(4));
                            list.Add(sc);
                        }
                    }
                    else
                    {
                        if (typeSlot.Type[0] + "" == "A")
                        {
                            var ts2 = _unitOfWork.TimeSlots.FindByCondition(s => s.Name == "slot2").FirstOrDefault();
                            var sc = Schedule.Create(Guid.NewGuid(), ts2.Id, request.TeacherId, course.Id, flagDate.AddDays(2));
                            list.Add(sc);
                        }
                        else
                        {
                            var ts2 = _unitOfWork.TimeSlots.FindByCondition(s => s.Name == "slot4").FirstOrDefault();
                            var sc = Schedule.Create(Guid.NewGuid(), ts2.Id, request.TeacherId, course.Id, flagDate.AddDays(2));
                            list.Add(sc);
                        }
                    }
                }

                flagDate = flagDate.AddDays(7);
            }

            _unitOfWork.Courses.Add(course);
            _unitOfWork.Schedules.AddRange(list);
            await _unitOfWork.SaveChangesAsync();
            return new Result(true, "Success", list);


        }


    }
}
