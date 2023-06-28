﻿using CareerOrientation.Domain.Entities;
using CareerOrientation.Infrastructure.Persistence.Repositories;

namespace CareerOrientation.Application.Common.Abstractions.Persistence;

public interface IUserRepository : IRepositoryBase
{
    Task<UniversityStudent?> GetUniversityStudentById(string? userId);
    Task AddUniversityStudent(UniversityStudent student);
}