using FPTCourseManagement.Application.Abtractions.Messaging;
using FPTCourseManagement.Application.Common.Service;
using FPTCourseManagement.Application.Module.Account.Queries;
using FPTCourseManagement.Domain.Entities.Users;
using FPTCourseManagement.Domain.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FPTCourseManagement.Application.Module.Account.Queries.QueryHandle
{
    public class LoginQueryHandle : IRequestHandler<LoginQuery, Result>
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IJwtService _jwtService;

        public LoginQueryHandle(IUnitOfWork unitofwork, IJwtService jwtService)
        {
            _unitofwork = unitofwork;
            _jwtService = jwtService;
        }
        public async Task<Result> Handle(LoginQuery request, CancellationToken cancellationToken)
        {
            string accesstoken = "";
            if (request.email is null)
                return new Result(false, "email null", new object());

            if (request.password is null)
                return new Result(false, "password null", new object());

            var ac = _unitofwork.Users.FindByCondition(s => s.Email.Equals(request.email) && s.Password.Equals(request.password)).Include(s => s.Role).FirstOrDefault();
            if (ac is null)
            {
                return new Result(false, "account is not found", new object());
            }
            return new Result(true, "login succes", (await _jwtService.generatejwttokentw(ac)));
        }

    }
}



      
