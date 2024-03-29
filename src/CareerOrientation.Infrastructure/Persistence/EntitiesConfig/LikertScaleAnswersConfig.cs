﻿using CareerOrientation.Domain.Entities;
using CareerOrientation.Domain.JunctionEntities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareerOrientation.Infrastructure.Persistence.EntitiesConfig;

public class LikertScaleAnswersConfig : IEntityTypeConfiguration<LikertScaleAnswers>
{
    public void Configure(EntityTypeBuilder<LikertScaleAnswers> builder)
    {
        builder.HasKey(x => x.LikertScaleAnswerId);
        
        builder.Property(x => x.Option1)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Option2)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Option3)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Option4)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.Option5)
            .IsRequired()
            .HasMaxLength(500);

        builder.HasMany(x => x.Questions)
            .WithMany(x => x.LikertScaleAnswers)
            .UsingEntity<QuestionLikertScaleAnswers>();
    }
}