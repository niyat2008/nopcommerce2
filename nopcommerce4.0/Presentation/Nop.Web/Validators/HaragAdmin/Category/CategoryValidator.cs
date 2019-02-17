using FluentValidation;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Services.Z_HaragAdmin.Categories;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Validators.HaragAdmin.Category
{
    public class CategoryValidator : BaseNopValidator<PostCategoryModel>
    {
        public CategoryValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(c => c.Name).NotNull().NotEmpty().WithMessage("ادخل الصنف").MaximumLength(4000);
        }
    }
}
