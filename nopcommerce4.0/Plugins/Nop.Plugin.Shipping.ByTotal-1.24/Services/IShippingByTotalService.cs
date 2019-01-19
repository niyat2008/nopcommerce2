using Nop.Core;
using Nop.Core.Domain.Shipping;
using Nop.Plugin.Shipping.ByTotal.Domain;

namespace Nop.Plugin.Shipping.ByTotal.Services
{
    /// <summary>
    /// Shipping By Total Service
    /// </summary>
    public partial interface IShippingByTotalService
    {
        /// <summary>
        /// Gets all the ShippingByTotalRecords
        /// </summary>
        /// <param name="pageIndex">Page index</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>ShippingByTotalRecord collection</returns>
        IPagedList<ShippingByTotalRecord> GetAllShippingByTotalRecords(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// Finds the ShippingByTotalRecord by its identifier
        /// </summary>
        /// <param name="shippingByTotalRecordId">ShippingByTotalRecord identifier</param>
        /// <returns>ShippingByTotalRecord</returns>
        ShippingByTotalRecord GetShippingByTotalRecordById(int shippingByTotalRecordId);

        /// <summary>
        /// Finds the ShippingByTotalRecord
        /// </summary>
        /// <param name="shippingMethodId">Shipping method identifier</param>
        /// <param name="storeId">Store identifier</param>
        /// <param name="warehouseId">Warehouse identifier</param>
        /// <param name="subtotal">Order's subtotal</param>
        /// <param name="countryId">Country identifier</param>
        /// <param name="subtotal">Subtotal</param>
        /// <param name="stateProvinceId">State / province identifier</param>
        /// <param name="zipPostalCode">ZIP / postal code</param>
        /// <returns>ShippingByTotalRecord</returns>
        ShippingByTotalRecord FindShippingByTotalRecord(int shippingMethodId, int storeId, int warehouseId,
            int countryId, decimal subtotal, int stateProvinceId, string zipPostalCode);

        /// <summary>
        /// Deletes the ShippingByTotalRecord
        /// </summary>
        /// <param name="shippingByTotalRecord">ShippingByTotalRecord</param>
        void DeleteShippingByTotalRecord(ShippingByTotalRecord shippingByTotalRecord);

        /// <summary>
        /// Inserts the ShippingByTotalRecord
        /// </summary>
        /// <param name="shippingByTotalRecord">ShippingByTotalRecord</param>
        void InsertShippingByTotalRecord(ShippingByTotalRecord shippingByTotalRecord);

        /// <summary>
        /// Updates the ShippingByTotalRecord
        /// </summary>
        /// <param name="shippingByTotalRecord">ShippingByTotalRecord</param>
        void UpdateShippingByTotalRecord(ShippingByTotalRecord shippingByTotalRecord);

        /// <summary>
        /// Delete ShippingByTotalRecords by ShippingMethod
        /// </summary>
        /// <remarks>Used when a ShippingMethod is deleted</remarks>
        /// <param name="shippingMethod">ShippingMethod</param>
        void DeleteByShippingMethod(ShippingMethod shippingMethod);
    }
}