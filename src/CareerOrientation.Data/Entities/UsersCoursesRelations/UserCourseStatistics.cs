﻿namespace CareerOrientation.Data.Entities.UsersCoursesRelations;

public class UserCourseStatistics
{
    public int CourseId { get; set; }
    public string UserId { get; set; }
    public int AccessCount { get; set; }
}
