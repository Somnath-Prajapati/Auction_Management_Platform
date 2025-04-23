using FluentValidation;

namespace AuctionManagementSystem.Application.Features.Settings.DirectSaleSettings.Commands.CreateDirectSaleSettings
{
    public class CreateDirectSaleSettingsCommandValidator : AbstractValidator<CreateDirectSaleSettingsCommand>
    {
        public CreateDirectSaleSettingsCommandValidator()
        {
            RuleFor(x => x.CartItemsLimit).GreaterThan(0);
            RuleFor(x => x.CartTimerInMinutes).GreaterThan(0);
        }
    }
}
