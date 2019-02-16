using FluentValidation;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Web.Validators.Harag.Post
{
    public class RatePostValidator : BaseNopValidator<RatePostModel>
    {
        public RatePostValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithErrorCode("Post Id is required")
                .GreaterThan(0).WithErrorCode("Post Id is digit grater than 0");
            RuleFor(x => x.Rate).NotNull().NotEmpty().WithErrorCode("Post rate is required");
        }
    }
}
