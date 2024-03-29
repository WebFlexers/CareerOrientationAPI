﻿using CareerOrientation.Application.Common.Abstractions.Persistence;
using CareerOrientation.Application.Tests.ProspectiveStudentTests.Common;
using CareerOrientation.Domain.Common.DomainErrors;

using ErrorOr;

using MediatR;

namespace CareerOrientation.Application.Tests.ProspectiveStudentTests.Queries.ProspectiveStudentTestsQuestions;

public class ProspectiveStudentTestsQuestionsHandler : 
    IRequestHandler<ProspectiveStudentTestsQuestionsQuery, ErrorOr<ProspectiveStudentTestResult>>
{
    private readonly ITestsRepository _testsRepository;

    public ProspectiveStudentTestsQuestionsHandler(ITestsRepository testsRepository)
    {
        _testsRepository = testsRepository;
    }
    
    public async Task<ErrorOr<ProspectiveStudentTestResult>> Handle(
        ProspectiveStudentTestsQuestionsQuery request, 
        CancellationToken cancellationToken)
    {
        var studentTestResult =
            await _testsRepository.GetGeneralTestQuestionsWithAnswers(request.GeneralTestId, cancellationToken);

        if (studentTestResult is null)
        {
            return Errors.Tests.NoQuestionsFound;
        }

        return studentTestResult;
    }
}