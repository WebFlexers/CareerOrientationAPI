﻿using CareerOrientation.API.Common.Contracts.Tests.ProspectiveStudentTests;
using CareerOrientation.API.Common.Mapping.Tests.Common;
using CareerOrientation.API.Common.Mapping.Tests.ProspectiveStudentTests;
using CareerOrientation.Application.Tests.ProspectiveStudentTests.Queries.GetProspectiveStudentTestsQuestions;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace CareerOrientation.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProspectiveStudentTestsController : ApiController
{
    private readonly IMediator _mediator;

    public ProspectiveStudentTestsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Gets the questions and answers of the general test whose id was provided
    /// </summary>
    /// <remarks>
    /// The available ids are 2:
    /// * 1 -> ComputerScienceSuitability
    /// * 2 -> UniversityOfPiraeusSuitability
    /// </remarks>
    [HttpGet("generalTestId")]
    public async Task<IActionResult> Get(int generalTestId, CancellationToken cancellationToken)
    {
        var query = new GetProspectiveStudentTestsQuestionsQuery(generalTestId);
        var result = await _mediator.Send(query, cancellationToken);

        return result.Match(test => Ok(test.MapToResponse()),
            errors => Problem(errors));
    }
    
    /// <summary>
    /// Submits the test answers of the prospective student
    /// </summary>
    /// <remarks>
    /// Rules: <br/>
    /// * In each question only one type of answer must be specified (either trueFalseAnswer, or multipleChoiceAnswerId 
    /// or likertScaleAnswer) <br/>
    /// * LikertScaleAnswers must be an integer from 1 to 5 <br/>
    /// * All questions of the given test must have an answer <br/> <br/>
    /// </remarks>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] ProspectiveStudentTestsSubmissionRequest request, 
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(request.MapToCommand(), cancellationToken);

        return result.Match(
            _ => Ok(),
            errors => Problem(errors));
    }
}