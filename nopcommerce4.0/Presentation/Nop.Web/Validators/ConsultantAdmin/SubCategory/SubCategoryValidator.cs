using FluentValidation;
using Nop.Services.Z_ConsultantAdmin.SubCategories;
using Nop.Web.Framework.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Validators.ConsultantAdmin.SubCategory
{
    public class SubCategoryValidator: BaseNopValidator<SubCategoryForPost>
    {
        public SubCategoryValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Enter SubCategory ").MaximumLength(4000); 
        }
    }
}
