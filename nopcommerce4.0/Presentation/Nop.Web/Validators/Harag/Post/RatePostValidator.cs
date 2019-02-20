using FluentValidation;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Framework.Validators;
using Nop.Web.Models.Harag.Rate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Web.Validators.Harag.Post
{
    public class RateValidator : BaseNopValidator<RateModel>
    {
        public RateValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.RateComment).NotEmpty().WithErrorCode("اضف نص التقييم");
                RuleFor(x => x.WhenBuyDone).NotNull().NotEmpty().WithErrorCode("اختر متي تم الشراء");
        }
    }
}
