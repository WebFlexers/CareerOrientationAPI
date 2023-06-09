﻿using CareerOrientation.Data.Entities.Specialties;
using CareerOrientation.Data.Entities.Tests.Enums;

namespace CareerOrientation.Data.Entities.Tests;

public class Question
{
    public int QuestionId { get; set; }
    public string Text { get; set; }
    public QuestionType Type { get; set; }

    public int? GeneralTestId { get; set; }
    public GeneralTest? GeneralTest { get; set; }
    public int? UniversityTestId { get; set; }
    public UniversityTest? UniversityTest { get; set; }

    public List<MastersDegree> MastersDegrees { get; set; }
    public List<Profession> Professions { get; set; }
    public List<Track> Tracks { get; set; }
}
