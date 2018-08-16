using FluentValidation.Attributes;
using Nop.Core.Domain.Vendors;
using Nop.Web.Areas.Admin.Models.Shipping;
using Nop.Web.Areas.Admin.Validators.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nop.Web.Areas.Admin.ViewModel
{

    //[Validator(typeof(WarehouseValidator))]
    public class VendorWareHouseViewModel
    {
        public VendorWareHouseViewModel()
        {
            this.WarehouseModel = new WarehouseModel();
            this.Vendors = new List<Vendor>();
        }

        public int VendorId { get; set; }
        public WarehouseModel WarehouseModel { get; set; }

        public List<Vendor> Vendors { get; set; }


    }
}
