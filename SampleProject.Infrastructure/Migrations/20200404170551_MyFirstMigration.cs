using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SampleProject.Infrastructure.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RegisteredCourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StudentId = table.Column<int>(nullable: false),
                    CourseId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegisteredCourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegisteredCourses_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "EndDate", "Name", "StartDate" },
                values: new object[,]
                {
                    { 1, "Awesome", new DateTime(2020, 4, 9, 21, 35, 50, 183, DateTimeKind.Local).AddTicks(5505), "c#", new DateTime(2020, 4, 4, 21, 35, 50, 165, DateTimeKind.Local).AddTicks(2659) },
                    { 2, "WOW", new DateTime(2020, 4, 9, 21, 35, 50, 183, DateTimeKind.Local).AddTicks(7779), "JavaScript", new DateTime(2020, 4, 4, 21, 35, 50, 183, DateTimeKind.Local).AddTicks(7676) }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, "John", "Cena" },
                    { 2, "Alex", "Morgan" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_RegisteredCourses_StudentId",
                table: "RegisteredCourses",
                column: "StudentId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "RegisteredCourses");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
