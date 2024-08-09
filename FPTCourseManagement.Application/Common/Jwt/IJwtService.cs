using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FPTCourseManagement.Domain.Entities;
using FPTCourseManagement.Domain.Entities.Users;
namespace FPTCourseManagement.Application.Common.Jwt
{
    public interface IJwtService
    {
        string GenerateJwtToken(User account, int expireTime = 30);
        Task<User?> VerifyToken(string token);
    }
}
