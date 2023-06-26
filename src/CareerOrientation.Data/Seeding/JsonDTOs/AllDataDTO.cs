﻿using CareerOrientation.Data.Entities.Specialties;
using CareerOrientation.Data.Entities.Tests;
using CareerOrientation.Data.Entities.TestsSpecialtiesRelations;

namespace CareerOrientation.Data.Seeding.JsonDTOs;

public class AllDataDTO
{
    public List<Question> Questions;
    public List<TrueFalseAnswer> TrueFalseAnswers;
    public List<MultipleChoiceAnswer> MultipleChoiceAnswers;
    public List<Track> Tracks;
    public List<MastersDegree> MastersDegrees;
    public List<Profession> Professions;
    public List<QuestionMastersDegree> QuestionMastersDegrees;
    public List<QuestionProfession> QuestionProfessions;
    public List<QuestionTrack> QuestionTracks;
    public List<GeneralTest> GeneralTests;
    public List<UniversityTest> UniversityTests;
}