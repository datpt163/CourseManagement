using FPTCourseManagement.Application.Abtractions.Messaging;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Module.Account.Queries
{
    public class LoginQuery : IRequest<Result>
    {
        public string? email { get; set; }
        public string? password { get; set; }
    }
}
