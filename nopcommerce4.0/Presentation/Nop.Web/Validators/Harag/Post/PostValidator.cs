using FluentValidation;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Services.Z_Harag.Post;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Validators.Harag.Post
{
    public class PostValidator : BaseNopValidator<PostForPostModel>
    {
        public PostValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Text).NotNull().NotEmpty().WithMessage("Post text is required").MaximumLength(4000);
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Post Title is required").MaximumLength(4000);
            RuleFor(x => x.CategoryId).NotNull().NotEmpty().WithErrorCode("Category Id is required")
                .GreaterThan(0).WithErrorCode("Category Id is digit grater than 0");

        }
    }
}
