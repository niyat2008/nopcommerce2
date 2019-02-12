
using FluentValidation;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Services.Z_ConsultantAdmin.Categories;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Validators.ConsultantAdmin.Category
{
    public class CategoryValidator:BaseNopValidator<CategoryModelForPost>
    {
        public CategoryValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Enter Category Name").MaximumLength(4000);
            RuleFor(x => x.Description).NotNull().NotEmpty().WithMessage("Enter Description").MaximumLength(4000);
        }
    }
}
