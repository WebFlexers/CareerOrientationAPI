﻿using CareerOrientation.Data.Entities.Specialties;
using CareerOrientation.Data.Entities.SpecialtiesRelations;
using CareerOrientation.Data.Entities.Tests;
using CareerOrientation.Data.Entities.TestsSpecialtiesRelations;
using CareerOrientation.Data.Entities.TestsUsersRelations;
using CareerOrientation.Data.Entities.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Reflection;
using CareerOrientation.Data.Entities.Courses;
using CareerOrientation.Data.Seeding;

namespace CareerOrientation.Data;

public class ApplicationDbContext : IdentityDbContext<User, IdentityRole, string>
{
    private readonly ILoggerFactory? _loggerFactory;
    private readonly IDataSeeding? _dataSeeding;

    public ApplicationDbContext(DbContextOptions options, ILoggerFactory? loggerFactory, 
        IDataSeeding? dataSeeding = null): base(options)
    {
        _loggerFactory = loggerFactory;
        _dataSeeding = dataSeeding;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        if (_loggerFactory != null)
        {
            optionsBuilder.UseLoggerFactory(_loggerFactory);
        }

        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override async void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        if (_dataSeeding == null)
        {
            var realData = new RealDataSeeding();
            await realData.Seed(builder);
        }
        else
        {
            await _dataSeeding.Seed(builder);
        }
    }

    // Specialties
    public DbSet<MastersDegree> MastersDegrees { get; set; }
    public DbSet<Profession> Professions { get; set; }
    public DbSet<Track> Tracks { get;set; }

    // Tests
    public DbSet<GeneralTest> GeneralTests { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<UniversityTest> UniversityTests { get; set; }

    public DbSet<MultipleChoiceAnswer> MultipleChoiceAnswers { get; set; }
    public DbSet<TrueFalseAnswer> TrueFalseAnswers { get; set; }

    // Specialty relations
    public DbSet<TrackMastersDegree> TrackMastersDegrees { get; set; }
    public DbSet<TrackProfession> TrackProfessions { get; set; }

    // Tests - Specialty relations
    public DbSet<QuestionMastersDegree> QuestionsMastersDegrees { get; set; }
    public DbSet<QuestionProfession> QuestionsProfessions { get; set; }
    public DbSet<QuestionTrack> QuestionsTracks { get; set; }

    // Answers - Users relations
    public DbSet<UserLikertScaleAnswer> UserLikertScaleAnswers { get; set; }    
    public DbSet<UserMultipleChoiceAnswer> UserMultipleChoiceAnswers { get; set; }
    public DbSet<UserTrueFalseAnswer> UserTrueFalseAnswers { get; set; }

    // Tests - Users relations
    public DbSet<UserTookGeneralTest> UsersTookGeneralTests { get; set; }
    public DbSet<StudentTookUniversityTest> StudentsTookUniversityTests { get; set; }

    // Users
    public DbSet<UniversityStudent> UniversityStudents { get; set; }
    
    // Courses
    public DbSet<Course> Courses { get; set; }
    public DbSet<Skill> Skills { get; set; }
    public DbSet<CourseSkill> CourseSkills { get; set; }
}