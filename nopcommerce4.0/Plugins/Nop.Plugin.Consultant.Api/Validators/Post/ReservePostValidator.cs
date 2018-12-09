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
    public class ReservePostValidator : BaseNopValidator<ReservePostModel>
    {
        public ReservePostValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.PostId).NotNull().NotEmpty().WithErrorCode("Post Id is required")
                .GreaterThan(0).WithErrorCode("Post Id is digit grater than 0");

        }
    }
}
