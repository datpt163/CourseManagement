using Capstone.Api.Common.ResponseApi.Controllers;
using FPTCourseManagement.Application.Module.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FPT_course_management.Module.Accounts.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : BaseController
    {

        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("auth")]
        public async Task<IActionResult> Login(LoginQuery loginQuery)
        {
            var result = await _mediator.Send(loginQuery);

            if (!result.IsSuccess)
                return ResponseBadRequest(result.Message);
            return ResponseOk(result.Data);
        }
    }
}
