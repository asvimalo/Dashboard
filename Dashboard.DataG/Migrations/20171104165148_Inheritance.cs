using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Dashboard.DataG.Migrations
{
    public partial class Inheritance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BaseEntity",
                columns: table => new
                {
                    EmployeeId = table.Column<int>(type: "int", nullable: true),
                    KnowledgeId = table.Column<int>(type: "int", nullable: true),
                    Assignment_EmployeeId = table.Column<int>(type: "int", nullable: true),
                    JobTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectId = table.Column<int>(type: "int", nullable: true),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StopDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LocationId = table.Column<int>(type: "int", nullable: true),
                    AssigmentId = table.Column<int>(type: "int", nullable: true),
                    Hours = table.Column<int>(type: "int", nullable: true),
                    Commitment_StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Commitment_StopDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PersonNr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Knowledge_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KnowledgeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comments = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhaseName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phase_ProjectId = table.Column<int>(type: "int", nullable: true),
                    Picture_EmployeeId = table.Column<int>(type: "int", nullable: true),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClientId = table.Column<int>(type: "int", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProjectName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Project_StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Project_StopDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeBudget = table.Column<int>(type: "int", nullable: true),
                    PhaseId = table.Column<int>(type: "int", nullable: true),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_EmployeeId",
                        column: x => x.EmployeeId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_KnowledgeId",
                        column: x => x.KnowledgeId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_Assignment_EmployeeId",
                        column: x => x.Assignment_EmployeeId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_LocationId",
                        column: x => x.LocationId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_AssigmentId",
                        column: x => x.AssigmentId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_Phase_ProjectId",
                        column: x => x.Phase_ProjectId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_Picture_EmployeeId",
                        column: x => x.Picture_EmployeeId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_ClientId",
                        column: x => x.ClientId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseEntity_BaseEntity_PhaseId",
                        column: x => x.PhaseId,
                        principalTable: "BaseEntity",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_EmployeeId",
                table: "BaseEntity",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_KnowledgeId",
                table: "BaseEntity",
                column: "KnowledgeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_Assignment_EmployeeId",
                table: "BaseEntity",
                column: "Assignment_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_ProjectId",
                table: "BaseEntity",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_LocationId",
                table: "BaseEntity",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_AssigmentId",
                table: "BaseEntity",
                column: "AssigmentId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_Phase_ProjectId",
                table: "BaseEntity",
                column: "Phase_ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_Picture_EmployeeId",
                table: "BaseEntity",
                column: "Picture_EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_ClientId",
                table: "BaseEntity",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_BaseEntity_PhaseId",
                table: "BaseEntity",
                column: "PhaseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseEntity");
        }
    }
}
