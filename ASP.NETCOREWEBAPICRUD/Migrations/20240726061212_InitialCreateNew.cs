using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASP.NETCOREWEBAPICRUD.Migrations
{
    public partial class InitialCreateNew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Designation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Category",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Tasks_User_Name",
                        column: x => x.Name,
                        principalTable: "User",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Work" },
                    { 2, "Home" },
                    { 3, "Leisure" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Name", "Address", "Designation", "Email", "Password" },
                values: new object[,]
                {
                    { "Chris", "New York", "Manager", "2021cs662@student.uet.edu.pk", "ABC2Inc" },
                    { "John", "New York", "Developer", "2021cs661@student.uet.edu.pk", "XYZ1Inc" },
                    { "Mukesh", "New Delhi", "Consultant", "2021cs663@student.uet.edu.pk", "XYZ3Inc" }
                });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "CategoryId", "Description", "DueDate", "IsCompleted", "Name", "Priority", "Title" },
                values: new object[] { 1, 1, "This is a seed task", new DateTime(2024, 8, 2, 11, 12, 11, 782, DateTimeKind.Local).AddTicks(2974), false, "John", 1, "Initial Task 1" });

            migrationBuilder.InsertData(
                table: "Tasks",
                columns: new[] { "TaskId", "CategoryId", "Description", "DueDate", "IsCompleted", "Name", "Priority", "Title" },
                values: new object[] { 2, 2, "This is another seed task", new DateTime(2024, 8, 5, 11, 12, 11, 782, DateTimeKind.Local).AddTicks(3004), false, "Chris", 2, "Initial Task 2" });

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_CategoryId",
                table: "Tasks",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_Name",
                table: "Tasks",
                column: "Name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
