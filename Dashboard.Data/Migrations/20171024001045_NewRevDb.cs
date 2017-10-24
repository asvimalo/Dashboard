using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dashboard.Data.Migrations
{
    public partial class NewRevDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Project_Commitments_CommitmentId",
                table: "Employee_Project");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Pictures_PictureId",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Employees_PictureId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Project_CommitmentId",
                table: "Employee_Project");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "PictureId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "CommitmentId",
                table: "Employee_Project");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Commitments");

            migrationBuilder.AddColumn<int>(
                name: "TimeBudget",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Comments",
                table: "Phases",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Employee_Project",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AssigmentId",
                table: "Commitments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Hours",
                table: "Commitments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Commitments_AssigmentId",
                table: "Commitments",
                column: "AssigmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commitments_Employee_Project_AssigmentId",
                table: "Commitments",
                column: "AssigmentId",
                principalTable: "Employee_Project",
                principalColumn: "AssignmentId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commitments_Employee_Project_AssigmentId",
                table: "Commitments");

            migrationBuilder.DropIndex(
                name: "IX_Commitments_AssigmentId",
                table: "Commitments");

            migrationBuilder.DropColumn(
                name: "TimeBudget",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Comments",
                table: "Phases");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Employee_Project");

            migrationBuilder.DropColumn(
                name: "AssigmentId",
                table: "Commitments");

            migrationBuilder.DropColumn(
                name: "Hours",
                table: "Commitments");

            migrationBuilder.AddColumn<int>(
                name: "Budget",
                table: "Projects",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PictureId",
                table: "Employees",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommitmentId",
                table: "Employee_Project",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Commitments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Pictures",
                columns: table => new
                {
                    PictureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(nullable: true),
                    FileName = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pictures", x => x.PictureId);
                    table.ForeignKey(
                        name: "FK_Pictures_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PictureId",
                table: "Employees",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Project_CommitmentId",
                table: "Employee_Project",
                column: "CommitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_EmployeeId",
                table: "Pictures",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Project_Commitments_CommitmentId",
                table: "Employee_Project",
                column: "CommitmentId",
                principalTable: "Commitments",
                principalColumn: "CommitmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Pictures_PictureId",
                table: "Employees",
                column: "PictureId",
                principalTable: "Pictures",
                principalColumn: "PictureId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
