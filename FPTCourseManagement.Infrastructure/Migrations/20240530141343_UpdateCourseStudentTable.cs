using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FPTCourseManagement.Infrastructure.Migrations
{
    public partial class UpdateCourseStudentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "course",
                newName: "CourseCode");

            migrationBuilder.RenameIndex(
                name: "IX_course_Name",
                table: "course",
                newName: "IX_course_CourseCode");

            migrationBuilder.AlterColumn<Guid>(
                name: "StudentCode",
                table: "student",
                type: "char(25)",
                fixedLength: true,
                maxLength: 25,
                nullable: false,
                collation: "ascii_general_ci",
                oldClrType: typeof(string),
                oldType: "char(25)",
                oldFixedLength: true,
                oldMaxLength: 25)
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CourseCode",
                table: "course",
                newName: "Name");

            migrationBuilder.RenameIndex(
                name: "IX_course_CourseCode",
                table: "course",
                newName: "IX_course_Name");

            migrationBuilder.AlterColumn<string>(
                name: "StudentCode",
                table: "student",
                type: "char(25)",
                fixedLength: true,
                maxLength: 25,
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "char(25)",
                oldFixedLength: true,
                oldMaxLength: 25)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "ascii_general_ci");
        }
    }
}
