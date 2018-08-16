using FluentValidation;
using Nop.Web.Areas.Admin.Models.Shipping;
using Nop.Core.Domain.Shipping;
using Nop.Data;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;
using Nop.Web.Areas.Admin.ViewModel;

namespace Nop.Web.Areas.Admin.Validators.Shipping
{
    public partial class VendorWareHouseViewModelValidator : BaseNopValidator<VendorWareHouseViewModel>
    {
        public VendorWareHouseViewModelValidator(ILocalizationService localizationService, IDbContext dbContext)
        {
            RuleFor(x => x.VendorId).NotEmpty().WithMessage(localizationService.GetResource("Admin.Configuration.Shipping.Warehouses.Fields.VendorId.Required"));

            


         
        }
    }
}