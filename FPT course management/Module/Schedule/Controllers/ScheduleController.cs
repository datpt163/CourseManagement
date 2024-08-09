using FPTCourseManagement.Domain.Entities.Users;
using FPTCourseManagement.Domain.Repository;
using FPTCourseManagement.Infrastructure.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FPT_course_management.Module.Schedule.Controllers
{
    [Route("api/schedule")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JWTService _jwtService;
        public ScheduleController(IUnitOfWork unitOfWork, JWTService jwtService = null)
        {
            _unitOfWork = unitOfWork;
            _jwtService = jwtService;
        }

        [HttpGet]
        [EnableQuery]
        [Authorize(Roles = "Teacher")]
        public IActionResult Getlist()
        {
            var x = _unitOfWork.Schedules.FindAll().Include(c => c.Course).Include(c => c.TimeSlot).ToList();

            if (x is null || x.Count() == 0)
                return Ok("Schedule is empty");

            List<ResponGetList> list = new List<ResponGetList>();
            foreach(var n in x)
            {
                list.Add(new ResponGetList() { Id = n.Id , Slot = n.TimeSlot.Name, CourseCode = n.Course.CourseCode, Date = n.DateOfLessson, TeacherId = n.TeacherId} );
            }
            return Ok(list);
        }

        //[HttpGet("ByToken")]
        //[EnableQuery]
        //[Authorize(Roles = "Teacher")]
        //public IActionResult Getlist2()
        //{
        //    var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
        //    if (token == null)
        //    {
        //        return BadRequest();
        //    }
        //    var us = _jwtService.VerifyToken(token);
        //    var x = _unitOfWork.Schedules.FindByCondition(x => x.TeacherId == us.Id).Include(c => c.Course).GroupBy(x => x.CourseId)
        //    .Select(g => g.First())
        //     .ToList();

        //    if (x is null || x.Count() == 0)
        //        return Ok("Schedule is empty");

        //    List<ResponGetList2> list = new List<ResponGetList2>();
        //    foreach (var n in x)
        //    {
        //        list.Add(new ResponGetList2() { Id = n.CourseId, CourseCode = n.Course.CourseCode});
        //    }
        //    return Ok( new ResponseAPI() { StatusCode = 200, Message = "", Data = list } );
        //}
    }

    public class ResponGetList
    {
        public Guid Id { get; set; }
        public string? Slot { get; set; }
        public string? CourseCode { get; set; }
        public DateTime Date { get; set; }
        public Guid TeacherId { get; set; }
    }

    public class ResponGetList2
    {
        public Guid Id { get; set; }
        public string? CourseCode { get; set; }
    }


    public class JWTService {

        private readonly IUnitOfWork _unitOfWork;

        public JWTService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public User VerifyToken(string token)
        {
            var secretKey = "ijurkbdlhmklqacwqzdxmkkhvqowlyqa99";
            var issuer = "localhost:7144";

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(secretKey);

            var validationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = issuer,
                ValidAudience = issuer,
                IssuerSigningKey = new SymmetricSecurityKey(key)
            };

            try
            {
                // Validate token and get principal
                var principal = tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                // Get claims from principal
                var userIdClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier) ;
                var roleClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role);

                if (userIdClaim == null || roleClaim == null)
                {
                    throw new SecurityTokenException("Invalid token");
                }


                var x = _unitOfWork.Users.FindByCondition(s => s.Id + "" == userIdClaim.Value).FirstOrDefault();
                if (x != null)
                    return x;
                return null;
            }
            catch (Exception ex)
            {
                // Handle or log the exception as needed
                throw new SecurityTokenException("Token validation failed", ex);
            }

        }
    }

}
