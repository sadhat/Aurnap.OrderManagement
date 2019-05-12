namespace Aurnap.OrderManagement.Core {
    public partial class PriceCalculatorManager {
        public interface IOrderRespository {
            Order GetBy(int orderNumber);
        }
    }
}
