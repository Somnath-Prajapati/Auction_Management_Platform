using AuctionManagementSystem.Application.Dtos.Settings;
using FluentValidation;

namespace AuctionManagementSystem.Application.Validators
{
    public class FooterLinksSettingsDtoValidator : AbstractValidator<FooterLinksSettingsDto>
    {
        public FooterLinksSettingsDtoValidator()
        {
            RuleFor(x => x.Faq)
                .NotEmpty().WithMessage("FAQ is required.");

            RuleFor(x => x.Blog)
                .NotEmpty().WithMessage("Blog is required.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Status is required.");

            RuleFor(x => x.TwitterLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.TwitterLink))
                .WithMessage("Twitter link must be a valid URL.");

            RuleFor(x => x.InstagramLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.InstagramLink))
                .WithMessage("Instagram link must be a valid URL.");

            RuleFor(x => x.FacebookLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.FacebookLink))
                .WithMessage("Facebook link must be a valid URL.");

            RuleFor(x => x.LinkedInLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.LinkedInLink))
                .WithMessage("LinkedIn link must be a valid URL.");

            RuleFor(x => x.YouTubeLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.YouTubeLink))
                .WithMessage("YouTube link must be a valid URL.");

            RuleFor(x => x.AppStoreLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.AppStoreLink))
                .WithMessage("App Store link must be a valid URL.");

            RuleFor(x => x.GooglePlayLink)
                .Must(BeAValidUrl).When(x => !string.IsNullOrWhiteSpace(x.GooglePlayLink))
                .WithMessage("Google Play link must be a valid URL.");
        }

        private bool BeAValidUrl(string url)
        {
            return Uri.IsWellFormedUriString(url, UriKind.Absolute);
        }
    }
}
