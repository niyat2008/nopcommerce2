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
    public class SearchValidator : BaseNopValidator<SearchModel>
    {
        public SearchValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Term).NotNull().NotEmpty().WithErrorCode("Shearch term  is required");
        }
    }
}
