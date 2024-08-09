﻿using Capstone.Application.Common.Jwt;
using FPTCourseManagement.Application.Common.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace FPTCourseManagement.Api.Common.ConfigureService
{
    public static class JwtAuthenExtention
    {
        public static void AddAuthSerivce(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            string secretKey = jwtSettings["SecretKey"] ?? string.Empty;
            string issuer = jwtSettings["Issuer"] ?? string.Empty;
            string audience = jwtSettings["Audience"] ?? string.Empty;
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ClockSkew = TimeSpan.Zero,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
                };
            });
            services.Configure<JwtSettings>(jwtSettings);
            services.AddScoped<IJwtService, JwtService>();
        }
    }
}
