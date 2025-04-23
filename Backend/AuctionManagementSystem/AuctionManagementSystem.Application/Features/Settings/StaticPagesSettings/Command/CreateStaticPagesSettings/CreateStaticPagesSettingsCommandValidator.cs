using FluentValidation;

namespace AuctionManagementSystem.Application.Features.Settings.StaticPagesSettings.Command.CreateStaticPagesSettings
{
    public class CreateStaticPagesSettingsCommandValidator : AbstractValidator<CreateStaticPagesSettingsCommand>
    {
        public CreateStaticPagesSettingsCommandValidator()
        {
            RuleFor(x => x.SettingsDto.PrivacyPolicy)
                .NotEmpty().WithMessage("Privacy policy is required.");

            RuleFor(x => x.SettingsDto.TermsAndConditions)
                .NotEmpty().WithMessage("Terms and conditions are required.");

            RuleFor(x => x.SettingsDto.CookiesPolicy)
                .NotEmpty().WithMessage("Cookies policy is required.");
        }
    }
}
