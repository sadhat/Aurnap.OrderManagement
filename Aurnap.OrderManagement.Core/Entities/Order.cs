using Aurnap.OrderManagement.Core.Entities;
using System.Collections.Generic;

namespace Aurnap.OrderManagement.Core {
    public sealed class Order {
        public Order(IEnumerable<OrderLineItem> orderLineItems, ShippingAddress shippingAddress) {
            OrderLineItems = orderLineItems;
            ShippingAddress = shippingAddress;
        }

        public IEnumerable<OrderLineItem> OrderLineItems { get; }
        public ShippingAddress ShippingAddress { get; }
    }
}