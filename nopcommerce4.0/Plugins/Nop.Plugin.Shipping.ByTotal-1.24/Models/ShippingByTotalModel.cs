using Nop.Web.Framework.Mvc.ModelBinding;
using Nop.Web.Framework.Mvc.Models;

namespace Nop.Plugin.Shipping.ByTotal.Models
{
    /// <summary>
    /// Model for ShippingByTotal entity
    /// </summary>
    public class ShippingByTotalModel : BaseNopEntityModel
    {
        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.Store")]
        public int StoreId { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.Store")]
        public string StoreName { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.Warehouse")]
        public int WarehouseId { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.Warehouse")]
        public string WarehouseName { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.Country")]
        public int CountryId { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.Country")]
        public string CountryName { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.StateProvince")]
        public int StateProvinceId { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.StateProvince")]
        public string StateProvinceName { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.ZipPostalCode")]
        public string ZipPostalCode { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.DisplayOrder")]
        public int DisplayOrder { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.ShippingMethod")]
        public int ShippingMethodId { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.ShippingMethod")]
        public string ShippingMethodName { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.From")]
        public decimal From { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.To")]
        public decimal To { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.UsePercentage")]
        public bool UsePercentage { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.ShippingChargePercentage")]
        public decimal ShippingChargePercentage { get; set; }

        [NopResourceDisplayName("Plugins.Shipping.ByTotal.Fields.ShippingChargeAmount")]
        public decimal ShippingChargeAmount { get; set; }
    }
}