using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dashboard.Data.Migrations
{
    public partial class NewDbv1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Commitments_Projects_UserId",
                table: "Commitments");

            migrationBuilder.DropForeignKey(
                name: "FK_Commitments_Users_UserId",
                table: "Commitments");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Users_UserId",
                table: "Pictures");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_UserId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Commitments_UserId",
                table: "Commitments");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Commitments");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Commitments");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Commitments");

            migrationBuilder.AddColumn<int>(
                name: "Budget",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Projects",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Notes",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProjectName",
                table: "Projects",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Pictures",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Commitments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartDate",
                table: "Commitments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StopDate",
                table: "Commitments",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonNr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "PictureId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Knowledges",
                columns: table => new
                {
                    KnowledgeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnowledgeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledges", x => x.KnowledgeId);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    LocationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Phases",
                columns: table => new
                {
                    PhaseId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Phases", x => x.PhaseId);
                    table.ForeignKey(
                        name: "FK_Phases_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee_Project",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CommitmentId = table.Column<int>(type: "int", nullable: false),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StopDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee_Project", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_Employee_Project_Commitments_CommitmentId",
                        column: x => x.CommitmentId,
                        principalTable: "Commitments",
                        principalColumn: "CommitmentId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Project_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employee_Project_Projects_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Projects",
                        principalColumn: "ProjectId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Knowledge_Employee",
                columns: table => new
                {
                    AcquiredKnowledgeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmployeeId = table.Column<int>(type: "int", nullable: false),
                    KnowledgeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Knowledge_Employee", x => x.AcquiredKnowledgeId);
                    table.ForeignKey(
                        name: "FK_Knowledge_Employee_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "EmployeeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Knowledge_Employee_Knowledges_KnowledgeId",
                        column: x => x.KnowledgeId,
                        principalTable: "Knowledges",
                        principalColumn: "KnowledgeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                    table.ForeignKey(
                        name: "FK_Clients_Location_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Location",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Tasks",
                columns: table => new
                {
                    TaskId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PhaseId = table.Column<int>(type: "int", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tasks", x => x.TaskId);
                    table.ForeignKey(
                        name: "FK_Tasks_Phases_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "Phases",
                        principalColumn: "PhaseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Projects_ClientId",
                table: "Projects",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_EmployeeId",
                table: "Pictures",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_LocationId",
                table: "Clients",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Project_CommitmentId",
                table: "Employee_Project",
                column: "CommitmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Project_EmployeeId",
                table: "Employee_Project",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Project_ProjectId",
                table: "Employee_Project",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PictureId",
                table: "Employees",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Employee_EmployeeId",
                table: "Knowledge_Employee",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Knowledge_Employee_KnowledgeId",
                table: "Knowledge_Employee",
                column: "KnowledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_Phases_ProjectId",
                table: "Phases",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_PhaseId",
                table: "Tasks",
                column: "PhaseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Employees_EmployeeId",
                table: "Pictures",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "EmployeeId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Projects_Clients_ClientId",
                table: "Projects",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Employees_EmployeeId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Projects_Clients_ClientId",
                table: "Projects");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Employee_Project");

            migrationBuilder.DropTable(
                name: "Knowledge_Employee");

            migrationBuilder.DropTable(
                name: "Tasks");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Knowledges");

            migrationBuilder.DropTable(
                name: "Phases");

            migrationBuilder.DropIndex(
                name: "IX_Projects_ClientId",
                table: "Projects");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_EmployeeId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Budget",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "Notes",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "ProjectName",
                table: "Projects");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Commitments");

            migrationBuilder.DropColumn(
                name: "StartDate",
                table: "Commitments");

            migrationBuilder.DropColumn(
                name: "StopDate",
                table: "Commitments");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Projects",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Projects",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Pictures",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Commitments",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Commitments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Commitments",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    PersonNr = table.Column<string>(nullable: true),
                    PictureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Users_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "PictureId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_UserId",
                table: "Pictures",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Commitments_UserId",
                table: "Commitments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PictureId",
                table: "Users",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Commitments_Projects_UserId",
                table: "Commitments",
                column: "UserId",
                principalTable: "Projects",
                principalColumn: "ProjectId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Commitments_Users_UserId",
                table: "Commitments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Users_UserId",
                table: "Pictures",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
