using System.Collections.Generic;

namespace Aurnap.OrderManagement.Core {
    public class Order {
        public Order(IEnumerable<OrderLineItem> orderLineItems, ShippingAddress shippingAddress) {
            OrderLineItems = orderLineItems;
            ShippingAddress = shippingAddress;
        }

        public IEnumerable<OrderLineItem> OrderLineItems { get; }
        public ShippingAddress ShippingAddress { get; }
    }
}