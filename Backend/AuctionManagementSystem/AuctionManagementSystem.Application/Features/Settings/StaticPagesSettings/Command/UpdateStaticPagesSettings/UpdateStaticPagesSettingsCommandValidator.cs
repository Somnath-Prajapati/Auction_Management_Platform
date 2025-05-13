using AuctionManagementSystem.Application.Dtos.Settings;
using FluentValidation;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.UpdateStaticPagesSettings
{
    public class UpdateStaticPagesSettingsCommandValidator : AbstractValidator<UpdateStaticPagesSettingsCommand>
    {
        public UpdateStaticPagesSettingsCommandValidator()
        {
            //RuleFor(x => x.Id).GreaterThan(0).WithMessage("Id must be greater than 0");

            //RuleFor(x => x.SettingsDto.PrivacyPolicy)
            //    .NotEmpty().WithMessage("Privacy Policy cannot be empty");

            //RuleFor(x => x.SettingsDto.TermsAndConditions)
            //    .NotEmpty().WithMessage("Terms and Conditions cannot be empty");

            //RuleFor(x => x.SettingsDto.CookiesPolicy)
            //    .NotEmpty().WithMessage("Cookies Policy cannot be empty");
        }
    }
}
