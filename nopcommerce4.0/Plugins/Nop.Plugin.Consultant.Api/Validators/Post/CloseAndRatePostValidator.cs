using FluentValidation;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Services.Z_Consultant.Post;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Consultant.Api.Validators.Post
{
    public class CloseAndRatePostValidator : BaseNopValidator<CloseAndRatePostModel>
    {
        public CloseAndRatePostValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            
            RuleFor(x => x.Id).NotNull().NotEmpty().WithErrorCode("Post Id is required")
                .GreaterThan(0).WithErrorCode("Post Id is digit grater than 0");
            RuleFor(x => x.Rate).GreaterThan(0).LessThan(6)
                .WithMessage("Rate must be digit betwwen 1-5");

        }
    }
}
