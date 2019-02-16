using FluentValidation;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Services.Z_Harag.Comment;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Validators.Harag.comment
{
    public class CommentValidator : BaseNopValidator<CommentForPostModel>
    {
        public CommentValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Text).NotNull().NotEmpty().WithMessage("Text of comment is required");
            RuleFor(x => x.PostId).NotNull().NotEmpty().WithMessage("Post Id is required, the comment must belong to post")
                .GreaterThan(0).WithMessage("The Post Id must be digit greater than 0");
        }
    }
}
