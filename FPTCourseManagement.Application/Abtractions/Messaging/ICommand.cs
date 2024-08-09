using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Abtractions.Messaging
{

    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public  Object Data { get; set; }

        public Result(bool isSucees, string message, Object data)
        {
            IsSuccess = isSucees;
            Message = message;
            Data = data;
        }   
    }

    public class Result<T>
    {
        public Result(bool isSucees, string message, T data)
        {
            IsSuccess = isSucees;
            Message = message;
            Data = data;
        }

        public bool IsSuccess { get; set; }
        public string? Message { get; set; }

        public T? Data { get; set; }
    }
}
