﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CareerOrientation.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    IsProspectiveStudent = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GeneralTests",
                columns: table => new
                {
                    GeneralTestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GeneralTests", x => x.GeneralTestId);
                });

            migrationBuilder.CreateTable(
                name: "LikertScaleAnswers",
                columns: table => new
                {
                    LikertScaleAnswerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Option1 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Option2 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Option3 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Option4 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Option5 = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikertScaleAnswers", x => x.LikertScaleAnswerId);
                });

            migrationBuilder.CreateTable(
                name: "MastersDegrees",
                columns: table => new
                {
                    MastersDegreeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MastersDegrees", x => x.MastersDegreeId);
                });

            migrationBuilder.CreateTable(
                name: "Professions",
                columns: table => new
                {
                    ProfessionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professions", x => x.ProfessionId);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                columns: table => new
                {
                    SkillId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Skills", x => x.SkillId);
                });

            migrationBuilder.CreateTable(
                name: "Tracks",
                columns: table => new
                {
                    TrackId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tracks", x => x.TrackId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsersTookGeneralTests",
                columns: table => new
                {
                    GeneralTestId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersTookGeneralTests", x => new { x.GeneralTestId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UsersTookGeneralTests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersTookGeneralTests_GeneralTests_GeneralTestId",
                        column: x => x.GeneralTestId,
                        principalTable: "GeneralTests",
                        principalColumn: "GeneralTestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: false),
                    TrackId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.CourseId);
                    table.ForeignKey(
                        name: "FK_Courses_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId");
                });

            migrationBuilder.CreateTable(
                name: "TrackMastersDegrees",
                columns: table => new
                {
                    TrackId = table.Column<int>(type: "integer", nullable: false),
                    MastersDegreeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackMastersDegrees", x => new { x.TrackId, x.MastersDegreeId });
                    table.ForeignKey(
                        name: "FK_TrackMastersDegrees_MastersDegrees_MastersDegreeId",
                        column: x => x.MastersDegreeId,
                        principalTable: "MastersDegrees",
                        principalColumn: "MastersDegreeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackMastersDegrees_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrackProfessions",
                columns: table => new
                {
                    TrackId = table.Column<int>(type: "integer", nullable: false),
                    ProfessionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackProfessions", x => new { x.TrackId, x.ProfessionId });
                    table.ForeignKey(
                        name: "FK_TrackProfessions_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "ProfessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrackProfessions_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UniversityStudents",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Semester = table.Column<int>(type: "integer", nullable: true),
                    IsGraduate = table.Column<bool>(type: "boolean", nullable: false),
                    TrackId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityStudents", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_UniversityStudents_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UniversityStudents_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId");
                });

            migrationBuilder.CreateTable(
                name: "UniversityTests",
                columns: table => new
                {
                    UniversityTestId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Semester = table.Column<int>(type: "integer", nullable: true),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    IsRevision = table.Column<bool>(type: "boolean", nullable: false),
                    TrackId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UniversityTests", x => x.UniversityTestId);
                    table.ForeignKey(
                        name: "FK_UniversityTests_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId");
                });

            migrationBuilder.CreateTable(
                name: "CourseSkills",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CourseSkills", x => new { x.CourseId, x.SkillId });
                    table.ForeignKey(
                        name: "FK_CourseSkills_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CourseSkills_Skills_SkillId",
                        column: x => x.SkillId,
                        principalTable: "Skills",
                        principalColumn: "SkillId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCourseGrade",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseGrade", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserCourseGrade_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourseGrade_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCourseStatistics",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    AccessCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCourseStatistics", x => new { x.CourseId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserCourseStatistics_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCourseStatistics_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "CourseId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "text", nullable: false),
                    Type = table.Column<int>(type: "integer", nullable: false),
                    GeneralTestId = table.Column<int>(type: "integer", nullable: true),
                    UniversityTestId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_Questions_GeneralTests_GeneralTestId",
                        column: x => x.GeneralTestId,
                        principalTable: "GeneralTests",
                        principalColumn: "GeneralTestId");
                    table.ForeignKey(
                        name: "FK_Questions_UniversityTests_UniversityTestId",
                        column: x => x.UniversityTestId,
                        principalTable: "UniversityTests",
                        principalColumn: "UniversityTestId");
                });

            migrationBuilder.CreateTable(
                name: "StudentsTookUniversityTests",
                columns: table => new
                {
                    UniversityTestId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentsTookUniversityTests", x => new { x.UniversityTestId, x.UserId });
                    table.ForeignKey(
                        name: "FK_StudentsTookUniversityTests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentsTookUniversityTests_UniversityTests_UniversityTestId",
                        column: x => x.UniversityTestId,
                        principalTable: "UniversityTests",
                        principalColumn: "UniversityTestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MultipleChoiceAnswers",
                columns: table => new
                {
                    MultipleChoiceAnswerId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Text = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    IsCorrect = table.Column<bool>(type: "boolean", nullable: false),
                    QuestionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MultipleChoiceAnswers", x => x.MultipleChoiceAnswerId);
                    table.ForeignKey(
                        name: "FK_MultipleChoiceAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionLikertScaleAnswers",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    LikertScaleAnswersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionLikertScaleAnswers", x => new { x.QuestionId, x.LikertScaleAnswersId });
                    table.ForeignKey(
                        name: "FK_QuestionLikertScaleAnswers_LikertScaleAnswers_LikertScaleAn~",
                        column: x => x.LikertScaleAnswersId,
                        principalTable: "LikertScaleAnswers",
                        principalColumn: "LikertScaleAnswerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionLikertScaleAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsMastersDegrees",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    MastersDegreeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsMastersDegrees", x => new { x.QuestionId, x.MastersDegreeId });
                    table.ForeignKey(
                        name: "FK_QuestionsMastersDegrees_MastersDegrees_MastersDegreeId",
                        column: x => x.MastersDegreeId,
                        principalTable: "MastersDegrees",
                        principalColumn: "MastersDegreeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsMastersDegrees_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsProfessions",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    ProfessionId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsProfessions", x => new { x.QuestionId, x.ProfessionId });
                    table.ForeignKey(
                        name: "FK_QuestionsProfessions_Professions_ProfessionId",
                        column: x => x.ProfessionId,
                        principalTable: "Professions",
                        principalColumn: "ProfessionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsProfessions_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsTracks",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    TrackId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsTracks", x => new { x.QuestionId, x.TrackId });
                    table.ForeignKey(
                        name: "FK_QuestionsTracks_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QuestionsTracks_Tracks_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Tracks",
                        principalColumn: "TrackId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TrueFalseAnswers",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    Value = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrueFalseAnswers", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK_TrueFalseAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLikertScaleAnswers",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLikertScaleAnswers", x => new { x.QuestionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserLikertScaleAnswers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserLikertScaleAnswers_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMultipleChoiceAnswers",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMultipleChoiceAnswers", x => new { x.QuestionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserMultipleChoiceAnswers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserMultipleChoiceAnswers_MultipleChoiceAnswers_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "MultipleChoiceAnswers",
                        principalColumn: "MultipleChoiceAnswerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTrueFalseAnswers",
                columns: table => new
                {
                    QuestionId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTrueFalseAnswers", x => new { x.QuestionId, x.UserId });
                    table.ForeignKey(
                        name: "FK_UserTrueFalseAnswers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserTrueFalseAnswers_TrueFalseAnswers_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "TrueFalseAnswers",
                        principalColumn: "QuestionId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TrackId",
                table: "Courses",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_CourseSkills_SkillId",
                table: "CourseSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_MultipleChoiceAnswers_QuestionId",
                table: "MultipleChoiceAnswers",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionLikertScaleAnswers_LikertScaleAnswersId",
                table: "QuestionLikertScaleAnswers",
                column: "LikertScaleAnswersId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_GeneralTestId",
                table: "Questions",
                column: "GeneralTestId");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_UniversityTestId",
                table: "Questions",
                column: "UniversityTestId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsMastersDegrees_MastersDegreeId",
                table: "QuestionsMastersDegrees",
                column: "MastersDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsProfessions_ProfessionId",
                table: "QuestionsProfessions",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsTracks_TrackId",
                table: "QuestionsTracks",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentsTookUniversityTests_UserId",
                table: "StudentsTookUniversityTests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackMastersDegrees_MastersDegreeId",
                table: "TrackMastersDegrees",
                column: "MastersDegreeId");

            migrationBuilder.CreateIndex(
                name: "IX_TrackProfessions_ProfessionId",
                table: "TrackProfessions",
                column: "ProfessionId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityStudents_TrackId",
                table: "UniversityStudents",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_UniversityTests_TrackId",
                table: "UniversityTests",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseGrade_UserId",
                table: "UserCourseGrade",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCourseStatistics_UserId",
                table: "UserCourseStatistics",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLikertScaleAnswers_UserId",
                table: "UserLikertScaleAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMultipleChoiceAnswers_UserId",
                table: "UserMultipleChoiceAnswers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UsersTookGeneralTests_UserId",
                table: "UsersTookGeneralTests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserTrueFalseAnswers_UserId",
                table: "UserTrueFalseAnswers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "CourseSkills");

            migrationBuilder.DropTable(
                name: "QuestionLikertScaleAnswers");

            migrationBuilder.DropTable(
                name: "QuestionsMastersDegrees");

            migrationBuilder.DropTable(
                name: "QuestionsProfessions");

            migrationBuilder.DropTable(
                name: "QuestionsTracks");

            migrationBuilder.DropTable(
                name: "StudentsTookUniversityTests");

            migrationBuilder.DropTable(
                name: "TrackMastersDegrees");

            migrationBuilder.DropTable(
                name: "TrackProfessions");

            migrationBuilder.DropTable(
                name: "UniversityStudents");

            migrationBuilder.DropTable(
                name: "UserCourseGrade");

            migrationBuilder.DropTable(
                name: "UserCourseStatistics");

            migrationBuilder.DropTable(
                name: "UserLikertScaleAnswers");

            migrationBuilder.DropTable(
                name: "UserMultipleChoiceAnswers");

            migrationBuilder.DropTable(
                name: "UsersTookGeneralTests");

            migrationBuilder.DropTable(
                name: "UserTrueFalseAnswers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Skills");

            migrationBuilder.DropTable(
                name: "LikertScaleAnswers");

            migrationBuilder.DropTable(
                name: "MastersDegrees");

            migrationBuilder.DropTable(
                name: "Professions");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "MultipleChoiceAnswers");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "TrueFalseAnswers");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "GeneralTests");

            migrationBuilder.DropTable(
                name: "UniversityTests");

            migrationBuilder.DropTable(
                name: "Tracks");
        }
    }
}
