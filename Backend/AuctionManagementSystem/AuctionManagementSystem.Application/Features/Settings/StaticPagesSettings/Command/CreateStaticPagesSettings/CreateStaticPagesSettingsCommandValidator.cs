using AuctionManagementSystem.Application.Dtos.Settings;
using FluentValidation;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.CreateStaticPagesSettings
{
    public class CreateStaticPagesSettingsCommandValidator : AbstractValidator<StaticPagesSettingsDto>
    {
        public CreateStaticPagesSettingsCommandValidator()
        {
            RuleFor(x => x.PrivacyPolicy)
                .NotEmpty().WithMessage("Privacy policy is required.");

            RuleFor(x => x.TermsAndConditions)
                .NotEmpty().WithMessage("Terms and conditions are required.");

            RuleFor(x => x.CookiesPolicy)
                .NotEmpty().WithMessage("Cookies policy is required.");
        }
    }
}
