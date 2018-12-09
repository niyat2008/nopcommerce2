using FluentValidation;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Services.Z_Consultant.Comment;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Consultant.Api.Validators.Comment
{
    public class DeleteCommentValidator  : BaseNopValidator<DeleteCommentModel>
    {
        public DeleteCommentValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.CommentId).NotNull().NotEmpty().WithMessage("Comment Id is required")
                .GreaterThan(0).WithMessage("The Comment Id must be digit greater than 0");
        }
    }
}
