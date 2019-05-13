namespace Aurnap.OrderManagement.Core.Entities {
    public sealed class OrderLineItem {
        public OrderLineItem(int orderId, int orderLineItemId, double price, int quantity, bool isBogoHalfOn, int? maxLimit) {
            OrderId = orderId;
            OrderLineItemId = orderLineItemId;
            Price = price;
            Quantity = quantity;
            IsBogoHalfPrice = isBogoHalfOn;
            BogoMaxLimit = maxLimit;
        }

        public int OrderId { get; }
        public int OrderLineItemId { get; }
        public double Price { get; }
        public int Quantity { get; }
        public bool IsBogoHalfPrice { get; }
        public int? BogoMaxLimit { get; }
    }
}