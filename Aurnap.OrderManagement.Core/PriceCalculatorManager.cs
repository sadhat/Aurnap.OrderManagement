using Aurnap.OrderManagement.Core.Entities;
using Aurnap.OrderManagement.Core.Interfaces;

namespace Aurnap.OrderManagement.Core {
    public sealed class PriceCalculatorManager {
        public PriceCalculatorManager(IOrderRespository orderRespository, ITaxService taxService) {
            OrderRespository = orderRespository;
            TaxService = taxService;
        }

        public double GetTotalPriceOf(int orderNumber) {
            Order order = OrderRespository.GetBy(orderNumber);
            double subTotal = 0;
            foreach (var orderLineItem in order.OrderLineItems) {
                subTotal += GetSubTotalOf(orderLineItem);
            }
            double taxRate = TaxService.GetSalesTaxRate(order.ShippingAddress);
            return subTotal + (subTotal * (taxRate / 100));
        }

        private static double GetSubTotalOf(OrderLineItem item) {
            if (!item.IsBogoHalfPrice) return item.Price * item.Quantity;

            var quantityOnSale = item.Quantity;
            if (item.BogoMaxLimit.HasValue && item.Quantity >= item.BogoMaxLimit) {
                quantityOnSale = item.BogoMaxLimit.Value;
            }
            quantityOnSale /= 2;
            var quantityOnRegular = item.Quantity - quantityOnSale;
            return (item.Price / 2 * quantityOnSale) + (item.Price * quantityOnRegular);
        }

        public IOrderRespository OrderRespository { get; }
        public ITaxService TaxService { get; }
    }
}
