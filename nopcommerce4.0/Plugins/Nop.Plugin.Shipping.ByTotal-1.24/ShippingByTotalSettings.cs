using Nop.Core.Configuration;

namespace Nop.Plugin.Shipping.ByTotal
{
    /// <summary>
    /// Settings for the "Shipping By Total" plugin
    /// </summary>
    public class ShippingByTotalSettings : ISettings
    {
        /// <summary>
        /// Wether returned shipping methods are limited to configured ones (if set to false, returns zero for unconfigured shipping methods)
        /// </summary>
        public bool LimitMethodsToCreated { get; set; }
    }
}