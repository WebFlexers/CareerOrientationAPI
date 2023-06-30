﻿using System.Collections.Immutable;

using CareerOrientation.Application.Common.Abstractions.Persistence;
using CareerOrientation.Application.Tests.ProspectiveStudentTests.Common;
using CareerOrientation.Application.Tests.ProspectiveStudentTests.Common.Mapping;
using CareerOrientation.Application.Tests.StudentTests.Common;
using CareerOrientation.Application.Tests.StudentTests.Common.Mapping;
using CareerOrientation.Domain.Common.DomainErrors;
using CareerOrientation.Domain.Common.Enums;
using CareerOrientation.Domain.Entities;
using CareerOrientation.Domain.Entities.Enums;
using CareerOrientation.Domain.JunctionEntities;

using ErrorOr;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CareerOrientation.Infrastructure.Persistence.Repositories;

public class TestsRepository : RepositoryBase, ITestsRepository
{
    public TestsRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<StudentTestResult?> GetSemesterTestQuestionsWithAnswers(int? semester, 
        string? track, CancellationToken cancellationToken)
    {
        StudentTestResult? studentTestResults;
        
        if (track is null)
        {
            studentTestResults = await _dbContext.UniversityTests
                .AsNoTracking()
                .Where(t => t.Semester == semester)
                .Include(t => t.Questions)
                .ThenInclude(q => q.MultipleChoiceAnswers)
                .Include(t => t.Questions)
                .ThenInclude(q => q.LikertScaleAnswers)
                .Select(test => test.MapToStudentTestResult())
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            studentTestResults = await _dbContext.UniversityTests
                .AsNoTracking()
                .Where(t => t.Semester == semester && t.Track!.Name == track)
                .Include(t => t.Questions)
                .ThenInclude(q => q.MultipleChoiceAnswers)
                .Include(t => t.Questions)
                .ThenInclude(q => q.LikertScaleAnswers)
                .Select(test => test.MapToStudentTestResult())
                .FirstOrDefaultAsync(cancellationToken);
        }
        
        return studentTestResults;
    }

    public async Task<StudentTestResult?> GetRevisionTestQuestionsWithAnswers(int? year, 
        string? track, CancellationToken cancellationToken)
    {
        StudentTestResult? studentTestResults;

        if (string.IsNullOrWhiteSpace(track))
        {
            studentTestResults = await _dbContext.UniversityTests
                .AsNoTracking()
                .Where(t => t.Year == year && t.IsRevision)
                .Include(t => t.Questions)
                .ThenInclude(q => q.MultipleChoiceAnswers)
                .Include(t => t.Questions)
                .ThenInclude(q => q.LikertScaleAnswers)
                .Select(test => test.MapToStudentTestResult())
                .FirstOrDefaultAsync(cancellationToken);
        }
        else
        {
            studentTestResults = await _dbContext.UniversityTests
                .AsNoTracking()
                .Where(t => t.Year == year && t.IsRevision && t.Track!.Name == track)
                .Include(t => t.Questions)
                .ThenInclude(q => q.MultipleChoiceAnswers)
                .Include(t => t.Questions)
                .ThenInclude(q => q.LikertScaleAnswers)
                .Select(test => test.MapToStudentTestResult())
                .FirstOrDefaultAsync(cancellationToken);
        }
   
        return studentTestResults;
    }

    public async Task<ProspectiveStudentTestResult?> GetGeneralTestQuestionsWithAnswers(
        int generalTestId, CancellationToken cancellationToken)
    {
        var generalTest = await _dbContext.GeneralTests
            .AsNoTracking()
            .Where(t => t.GeneralTestId == generalTestId)
            .Include(t => t.Questions)
                .ThenInclude(q => q.MultipleChoiceAnswers)
            .Include(t => t.Questions)
                .ThenInclude(q => q.LikertScaleAnswers)
            .Select(test => test.MapToProspectiveStudentTestResult())
            .FirstOrDefaultAsync(cancellationToken);

        return generalTest;
    }

    public async Task<ErrorOr<Unit>> InsertUserTestAnswers(
        string userId,
        int testId, 
        TestType testType,
        List<QuestionAnswer> answers, 
        CancellationToken cancellationToken)
    {
        var correctTestTypeResult = await EnsureCorrectTestType(testId, testType, cancellationToken);
        if (correctTestTypeResult.IsError)
        {
            return correctTestTypeResult;
        }
        
        var userHasTakenTestResult = await EnsureUserHasntTakenTest(userId, testId, testType, cancellationToken);
        if (userHasTakenTestResult.IsError)
        {
            return userHasTakenTestResult;
        }
        
        var answersValidityResult = await CheckAnswersValidity(testId, testType, answers, cancellationToken);
        if (answersValidityResult.IsError)
        {
            return answersValidityResult;
        }

        await InsertAnswersToDb(userId, answers);

        if (testType == TestType.UniversityTest)
        {
            await InsertToStudentTookUniversityTest(userId, testId);
        }
        else
        {
            await InsertToUserTookGeneralTest(userId, testId);
        }

        await _dbContext.SaveChangesAsync(CancellationToken.None);

        return Unit.Value;
    }

    private async Task<ErrorOr<Unit>> EnsureCorrectTestType(int testId, TestType testType, 
        CancellationToken cancellationToken)
    {
        if (testType == TestType.UniversityTest)
        {
            var universityTest = await _dbContext.UniversityTests.FindAsync(
                new object?[] { testId }, cancellationToken);
            if (universityTest is null)
            {
                return Errors.Tests.UniversityTestIdNotFound;
            }
        }
        else
        {
            var generalTest = await _dbContext.GeneralTests.FindAsync(
                new object?[] { testId }, cancellationToken);
            if (generalTest is null)
            {
                return Errors.Tests.GeneralTestNotFound;
            }
        }

        return Unit.Value;
    }
    private async Task<ErrorOr<Unit>> EnsureUserHasntTakenTest(string userId, int testId, TestType testType,
        CancellationToken cancellationToken)
    {
        if (testType == TestType.UniversityTest)
        {
            var studentResult = await _dbContext.StudentsTookUniversityTests.FindAsync(
                new object?[] { testId, userId }, cancellationToken);
            if (studentResult is not null)
            {
                return Errors.Tests.StudentAlreadyTookTest;
            }
        }
        else
        {
            var generalUserResult = await _dbContext.UsersTookGeneralTests.FindAsync(
                new object?[] { testId, userId }, cancellationToken);
            if (generalUserResult is not null)
            {
                return Errors.Tests.ProspectiveStudentAlreadyTookTest;
            }
        }

        return Unit.Value;
    }
    private async Task<ErrorOr<Unit>> CheckAnswersValidity(int testId, TestType testType,
        List<QuestionAnswer> userAnswers, CancellationToken cancellationToken)
    {
        var testQuestionsQueryable = _dbContext.Questions.AsNoTracking();

        testQuestionsQueryable = testType == TestType.UniversityTest 
            ? testQuestionsQueryable.Where(q => q.UniversityTestId == testId) 
            : testQuestionsQueryable.Where(q => q.GeneralTestId == testId);
        
        var testQuestions = await testQuestionsQueryable.ToListAsync(cancellationToken);

        if (testQuestions.Count != userAnswers.Count)
        {
            return Errors.Tests.NotAllQuestionsWereAnswered;
        }

        foreach (var userAnswer in userAnswers)
        {
            var question = testQuestions.FirstOrDefault(q => q.QuestionId == userAnswer.QuestionId);
            
            if (question is null)
            {
                return Errors.Tests.QuestionNotPartOfTest(testId, userAnswer.QuestionId);
            }

            if (question.Type != userAnswer.QuestionType)
            {
                return Errors.Tests.AnswerTypeNotCompatible(
                    userAnswer.QuestionId, userAnswer.QuestionType.ToString(), question.Type.ToString());
            }
        }

        var existingMultipleChoiceAnswersQueryable = _dbContext.Questions.AsNoTracking();

        existingMultipleChoiceAnswersQueryable = testType == TestType.UniversityTest 
            ? existingMultipleChoiceAnswersQueryable.Where(q => q.UniversityTestId == testId) 
            : existingMultipleChoiceAnswersQueryable.Where(q => q.GeneralTestId == testId);

        // Here we check if the multiple choice answer ids that the user gave exist in the database and correspond
        // to the correct question
        var existingMultipleChoiceAnswers = await existingMultipleChoiceAnswersQueryable
            .Select(q => q.MultipleChoiceAnswers)
            .ToListAsync(cancellationToken);

        // If there are no multiple choice answers we are fine
        if (existingMultipleChoiceAnswers.Any() == false)
        {
            return Unit.Value;
        }
        
        // If there is any user answer that is a multiple choice answer and it doesn't exist in the database
        // answers that correspond to the given test then the multiple choice given by the user is wrong
        foreach (var userAnswer in userAnswers)
        {
            if (userAnswer.QuestionType != QuestionType.MultipleChoice)
            {
                continue;
            }

            if (existingMultipleChoiceAnswers.Any(existingAnswer => existingAnswer.Any(
                    a => a.MultipleChoiceAnswerId == userAnswer.MultipleChoiceAnswerId)) == false)
            {
                return Errors.Tests.MultipleChoiceAnswerDoesntMatch(userAnswer.QuestionId);
            }
        }

        return Unit.Value;
    }
    private Task InsertAnswersToDb(string userId, List<QuestionAnswer> answers)
    {
        var userTrueFalseAnswers = 
            answers.Where(a => a.TrueOrFalseAnswer is not null).ToImmutableArray();
        var userMultipleChoiceAnswers = 
            answers.Where(a => a.MultipleChoiceAnswerId is not null).ToImmutableArray();
        var userLikertScaleAnswers =
            answers.Where(a => a.LikertScaleAnswer is not null).ToImmutableArray();

        List<Task> addAnswersTasks = new();

        if (userTrueFalseAnswers.Any())
        {
            addAnswersTasks.Add(_dbContext.UserTrueFalseAnswers.AddRangeAsync(
                userTrueFalseAnswers.Select(a => a.MapToUserTrueFalseAnswer(userId))));
        }

        if (userMultipleChoiceAnswers.Any())
        {
            addAnswersTasks.Add(_dbContext.UserMultipleChoiceAnswers.AddRangeAsync(
                userMultipleChoiceAnswers.Select(a => a.MapToUserMultipleChoiceAnswer(userId))));
        }

        if (userLikertScaleAnswers.Any())
        {
            addAnswersTasks.Add(_dbContext.UserLikertScaleAnswers.AddRangeAsync(
                userLikertScaleAnswers.Select(a => a.MapToUserLikertScaleAnswer(userId))));
        }
        
        return Task.WhenAll(addAnswersTasks);
    }
    private ValueTask<EntityEntry<StudentTookUniversityTest>> InsertToStudentTookUniversityTest(
        string userId, int universityTestId)
    {
        return _dbContext.StudentsTookUniversityTests.AddAsync(
            new StudentTookUniversityTest(userId, universityTestId));
    }
    private ValueTask<EntityEntry<UserTookGeneralTest>> InsertToUserTookGeneralTest(string userId, int generalTestId)
    {
        return _dbContext.UsersTookGeneralTests.AddAsync(
            new UserTookGeneralTest(userId, generalTestId));
    }
}