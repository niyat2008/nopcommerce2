using Nop.Core.Domain.Shipping;
using Nop.Core.Events;
using Nop.Plugin.Shipping.ByTotal.Services;
using Nop.Services.Events;

namespace Nop.Plugin.Shipping.ByTotal.EventConsumers
{
    /// <summary>
    /// Event Consumer of ShippingMethod EntityDeleted Event
    /// </summary>
    /// <remarks>Used to remove ShippingByTotal records when their associated ShippingMethod is deleted.</remarks>
    public class ShippingMethodDeletedEventConsumer : IConsumer<EntityDeleted<ShippingMethod>>
    {
        #region Fields

        private readonly IShippingByTotalService _shippingByTotalService;

        #endregion Fields

        #region Ctor

        public ShippingMethodDeletedEventConsumer(IShippingByTotalService shippingByTotalService) => _shippingByTotalService = shippingByTotalService;

        #endregion Ctor

        #region IConsumer Methods

        public void HandleEvent(EntityDeleted<ShippingMethod> eventMessage)
        {
            var shippingMethod = eventMessage?.Entity;
            if (shippingMethod == null)
            {
                return;
            }

            //remove records that used the deleted ShippingMethod
            _shippingByTotalService.DeleteByShippingMethod(shippingMethod);
        }

        #endregion IConsumer Methods
    }
}