namespace Aurnap.OrderManagement.Core {
    public class ShippingAddress {
        public ShippingAddress(string street, string city, string state, string zipCode) {
            Street = street;
            City = city;
            State = state;
            ZipCode = zipCode;
        }

        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }
    }
}