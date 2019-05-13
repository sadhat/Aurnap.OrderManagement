namespace Aurnap.OrderManagement.Core.Interfaces {
    public interface IOrderRespository {
        Order GetBy(int orderNumber);
    }
}
