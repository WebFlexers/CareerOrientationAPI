﻿using CareerOrientation.Data.DTOs.Auth;
using FluentValidation;
using static CareerOrientation.Services.Validation.Common.ValidationHelper;

namespace CareerOrientation.Services.Validation.Auth;

public class CreateUserRequestValidator : AbstractValidator<CreateUserRequest>
{
    public CreateUserRequestValidator()
    {
        RuleFor(user => user.IsProspectiveStudent).NotNull()
            .WithErrorCode(UserErrorCodes.UserIdentifierRequired)
            .WithMessage("Πρέπει να προσδιοριστεί εάν ο χρήστης ανήκει στην κατηγορία των ενδιαφερόμενων");

        RuleFor(user => user.Password).NotEmpty()
            .WithErrorCode(UserErrorCodes.PasswordRequired)
            .WithMessage("Ο κωδικός πρόσβασης δεν μπορεί να είναι κενός");
        
        RuleFor(user => user.ConfirmPassword).NotEmpty()
            .WithErrorCode(UserErrorCodes.PasswordVerificationRequired)
            .WithMessage("Η επαλήθευση του κωδικού πρόσβασης δεν μπορεί να είναι κενή");

        RuleFor(user => user.Password).Equal(user => user.ConfirmPassword)
            .WithErrorCode(UserErrorCodes.InvalidPasswordVerification)
            .WithMessage("Ο κωδικός και η επαλήθευση κωδικού πρέπει να ταυτίζονται");

        When(user => user.IsProspectiveStudent, () =>
        {
            RuleFor(user => user.IsGraduate).Must(isGraduate => isGraduate == false)
                .WithErrorCode(UserErrorCodes.InvalidUserType)
                .WithMessage("Ο ενδιαφερόμενος χρήστης δεν μπορεί να είναι και απόφοιτος");
            RuleFor(user => user.Semester).Must(semester => semester == null)
                .WithErrorCode(UserErrorCodes.InvalidSemester)
                .WithMessage("Ο ενδιαφερόμενος χρήστης δεν βρίσκεται σε κάποιο εξάμηνο");
            RuleFor(user => user.Track).Null()
                .WithErrorCode(UserErrorCodes.InvalidTrack)
                .WithMessage("Ο ενδιαφερόμενος χρήστης δεν μπορεί να ανήκει σε κάποια κατεύθυνση");
        });

        When(user => user.IsProspectiveStudent == false, () =>
        {
            RuleFor(user => user.IsGraduate).NotNull()
                .WithErrorCode(UserErrorCodes.UserIdentifierRequired)
                .WithMessage("Πρέπει να προσδιοριστεί εάν ο φοιτητής έχει αποφοιτήσει");

            When(user => user.IsGraduate == false, () =>
            {
                RuleFor(user => user.Semester).Must(BeValidSemester)
                    .WithErrorCode(UserErrorCodes.InvalidSemester)
                    .WithMessage("Οι φοιτητές πρέπει να προσδιορίζουν το εξάμηνο, "+
                                 "στο οποίο βρίσκονται (από 1 έως 8)");
            });

            When(user => (user.Semester >= 5 && user.Semester <= 8) ||
                user.IsGraduate, () =>
                {
                    RuleFor(user => user.Track).Must(BeValidTrack)
                        .WithErrorCode(UserErrorCodes.InvalidTrack)
                        .WithMessage("Οι φοιτητές και οι απόφοιτοι πρέπει να προσδιορίζουν " +
                                     "μία από τις κατευθύνσεις: ΤΛΕΣ, ΔΥΣ, ΠΣΥ");
                });

            When(user => user.Semester >= 1 && user.Semester <= 4, () =>
            {
                RuleFor(user => user.Track).Null()
                    .WithErrorCode(UserErrorCodes.InvalidTrack)
                    .WithMessage("Οι φοιτητές μέχρι το 4ο εξάμηνο δεν έχουν επιλέξει ακόμη κατεύθυνση");
            });
        });
    }
}