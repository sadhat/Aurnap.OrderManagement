using Aurnap.OrderManagement.Core.Entities;

namespace Aurnap.OrderManagement.Core.Interfaces {
    public interface ITaxService {
        double GetSalesTaxRate(ShippingAddress shippingAddress);
    }
}
