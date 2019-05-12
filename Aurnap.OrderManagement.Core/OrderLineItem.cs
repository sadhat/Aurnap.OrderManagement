namespace Aurnap.OrderManagement.Core {
    public class OrderLineItem {
        public OrderLineItem(int orderId, int orderLineItemId, decimal price, int quantity, bool isBogoHalfOn, int? maxLimit) {
            OrderId = orderId;
            OrderLineItemId = orderLineItemId;
            Price = price;
            Quantity = quantity;
            IsBogoHalfPrice = isBogoHalfOn;
            BogoMaxLimit = maxLimit;
        }

        public int OrderId { get; }
        public int OrderLineItemId { get; }
        public decimal Price { get; }
        public int Quantity { get; }
        public bool IsBogoHalfPrice { get; }
        public int? BogoMaxLimit { get; }
    }
}