using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Contracts.User;
using AuctionManagementSystem.Application.Features.UserFeature.Command.CreateUser;
using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace AuctionManagementSystem.Application.Validators
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        private readonly ICountryRepository _countryRepository;
        private static readonly string[] AllowedExtensions = [".png", ".jpg", ".jpeg"];
        private const long MaxFileSize = 2 * 1024 * 1024; //for 2 mb size
        public CreateUserCommandValidator(ICountryRepository countryRepository)
        {
            _countryRepository = countryRepository;

            RuleFor(x => x.UserDto.Name)
                .NotEmpty().WithMessage("Please enter a valid Name");

            RuleFor(x => x.UserDto.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Please enter a valid Email address");

            RuleFor(x => x.UserDto.MobileNumber)
                .NotEmpty().WithMessage("Mobile number is required")
                .MustAsync(ValidateMobileNumber).WithMessage("Invalid mobile number for selected country");

            RuleFor(x => x.UserDto.PersonalIdNumber)
                .NotEmpty().WithMessage("This field is required");

            RuleFor(x => x.UserDto.Gender)
                .NotEmpty().WithMessage("This field is required")
                .Must(g => g == "Male" || g == "Female")
                .WithMessage("Gender must be either 'Male' or 'Female'");

            RuleFor(x => x.UserDto.PersonalIdExpiryDate)
                .NotEmpty().WithMessage("This field is required")
                .Must(date => date > DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("Expiry date must be in the future");
            RuleFor(x => x.UserDto.ProfileImage)
                .Must(BeAValidImage).WithMessage("Profile image must be .png, .jpg, or .jpeg and less than 2MB")
                .When(x => x.UserDto.ProfileImage != null);
            RuleFor(x => x.UserDto.PersonalIdImage)
                .Must(BeAValidImage).WithMessage("ID image must be .png, .jpg, or .jpeg and less than 2MB")
                .When(x => x.UserDto.PersonalIdImage != null);

        }
        private bool BeAValidImage(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            return file.Length <= MaxFileSize && AllowedExtensions.Contains(extension);
        }

        private async Task<bool> ValidateMobileNumber(CreateUserCommand command, string mobile, CancellationToken ct)
        {
            var country = await _countryRepository.GetCountryById(command.UserDto.CountryId);
            if (country == null) return false;

            if (mobile.Length < country.MinLength || mobile.Length > country.MaxLength)
            {
                return false;
            }

            var validStarts = country.SeriesStart?.Split(',') ?? Array.Empty<string>();
            if (!validStarts.Any(start => mobile.StartsWith(start)))
            {
                return false;
            }

            return true;
        }

    }
}