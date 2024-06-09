using FPT_course_management.Common.Controllers;
using FPTCourseManagement.Application.Module.Account.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace FPT_course_management.Module.Accounts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("Authenticate")]
        public async Task<IActionResult> Login(LoginQuery loginQuery)
        {
            var result = await _mediator.Send(loginQuery);

            if (!result.IsSuccess)
                return Ok(new ResponseAPI() { StatusCode = 400, Message = result.Message, Data = null });
            return Ok(new ResponseAPI() { StatusCode = 200, Message = result.Message, Data = result.Data });
        }
    }
}
