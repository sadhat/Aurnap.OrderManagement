using System;

namespace Aurnap.OrderManagement.Core {
    public partial class PriceCalculatorManager {
        public PriceCalculatorManager(IOrderRespository orderRespository, IPricingService pricingService) {
            OrderRespository = orderRespository;
            PricingService = pricingService;
        }

        public decimal GetTotal(int orderNumber) {
            Order order = OrderRespository.GetBy(orderNumber);
            decimal total = 0;
            foreach (var item in order.OrderLineItems) {
                if (item.IsBogoHalfPrice) {
                    if (item.BogoMaxLimit.HasValue) {
                        total += item.Price * (decimal)Math.Ceiling(Math.Abs(item.Quantity - item.BogoMaxLimit.Value) / 2.0);
                        if (item.BogoMaxLimit > item.Quantity) {
                            total += item.Price * (decimal)(item.BogoMaxLimit - item.Quantity);
                        }
                    }
                }
                else {
                    total += item.Price * item.Quantity;
                }
            }
            return total;
        }

        public IOrderRespository OrderRespository { get; }
        public IPricingService PricingService { get; }
    }
}
