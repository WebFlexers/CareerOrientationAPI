﻿using CareerOrientation.Application.Common.Abstractions.Persistence;
using CareerOrientation.Domain.Common.DomainErrors;
using CareerOrientation.Domain.Common.Enums;

using ErrorOr;

using MediatR;

namespace CareerOrientation.Application.Tests.ProspectiveStudentTests.Queries.HasProspectiveStudentTakenTest;

public class HasProspectiveStudentTakenTestHandler : 
    IRequestHandler<HasProspectiveStudentTakenTestQuery, ErrorOr<bool>>
{
    private readonly ITestsRepository _testsRepository;

    public HasProspectiveStudentTakenTestHandler(ITestsRepository testsRepository)
    {
        _testsRepository = testsRepository;
    }
    
    public async Task<ErrorOr<bool>> Handle(HasProspectiveStudentTakenTestQuery request, 
        CancellationToken cancellationToken)
    {
        var result = await _testsRepository.EnsureUserHasntTakenTest(
            request.UserId, request.GeneralTestId, TestType.GeneralTest, cancellationToken);

        if (result.IsError == false)
        {
            // If there were no errors it means that the user hasn't taken the test
            return false;
        }
        
        if (result.Errors.Any(e => e.Code == Errors.Tests.ProspectiveStudentAlreadyTookTest.Code))
        {
            return true;
        }

        return result.Errors;
    }
}