using FPTCourseManagement.Application;
using Microsoft.AspNetCore.Mvc.Formatters;
using System.Reflection;
using FPTCourseManagement.Application.Module.Courses.Commands;
using FPTCourseManagement.Domain.Repository;
using FPTCourseManagement.Infrastructure.Repository;
using FPTCourseManagement.Domain.Entities.Courses;
using FPTCourseManagement.Domain.Entities.Roles;
using FPTCourseManagement.Domain.Entities.Schedules;
using FPTCourseManagement.Domain.Entities.Slot;
using FPTCourseManagement.Domain.Entities.Subjects;
using FPTCourseManagement.Domain.Entities.Users;
using FPTCourseManagement.Infrastructure.DbContexts;
using Microsoft.AspNetCore.OData;
using Microsoft.AspNetCore.Hosting;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using FPTCourseManagement.Application.Common.Service;
using FPT_course_management.Module.Schedule.Controllers;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
#region register repo service
builder.Services.AddControllers().AddOData(opt => opt.Select().Filter().OrderBy());
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IMapper, Mapper>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IRepository<Course>, Repository<Course>>();
builder.Services.AddTransient<IRepository<CourseStudent>, Repository<CourseStudent>>();
builder.Services.AddTransient<IRepository<Role>, Repository<Role>>();
builder.Services.AddTransient<IRepository<RolePermission>, Repository<RolePermission>>();
builder.Services.AddTransient<IRepository<Permission>, Repository<Permission>>();
builder.Services.AddTransient<IRepository<Attendance>, Repository<Attendance>>();
builder.Services.AddTransient<IRepository<Schedule>, Repository<Schedule>>();
builder.Services.AddTransient<IRepository<TimeSlot>, Repository<TimeSlot>>();
builder.Services.AddTransient<IRepository<TypeSlot>, Repository<TypeSlot>>();
builder.Services.AddTransient<IRepository<Subject>, Repository<Subject>>();
builder.Services.AddTransient<IRepository<Student>, Repository<Student>>();
builder.Services.AddTransient<IRepository<Teacher>, Repository<Teacher>>();
builder.Services.AddTransient<IRepository<User>, Repository<User>>();
builder.Services.AddTransient<AppDbContext, AppDbContext>();
#endregion

#region register jwt author service
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
string secretKey = jwtSettings["SecretKey"];
string issuer = jwtSettings["Issuer"];
builder.Services.AddAuthentication(options =>
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
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = issuer,
        ValidAudience = issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});
builder.Services.AddTransient<IJwtService, JwtService>();
#endregion

builder.Services.AddApplication();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(FPTCourseManagement.Application.AssemblyReference.Assembly));

builder.Services.AddTransient<JWTService, JWTService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
