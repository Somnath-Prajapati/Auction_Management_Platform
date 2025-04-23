using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuctionManagementSystem.Application.Dtos.Settings;
using FluentValidation;


namespace AuctionManagementSystem.Application.Features.Settings.SystemSettings.Validators
{
    public class SystemSettingsValidator : AbstractValidator<SystemSettingsDto>
    {
        public SystemSettingsValidator()
        {
            RuleFor(x => x.GlobalIncrementalTimeInMinutes).GreaterThanOrEqualTo(0);  //validatio for the incremental time
        }
    }
}
