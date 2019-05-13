using Aurnap.OrderManagement.Core.Entities;
using Aurnap.OrderManagement.Core.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace Aurnap.OrderManagement.Core.Tests {
    public class PriceCalculatorManagerTests {
        readonly Mock<IOrderRespository> orderRepository = new Mock<IOrderRespository>();
        readonly Mock<ITaxService> taxService = new Mock<ITaxService>();
        PriceCalculatorManager sut;

        [SetUp]
        public void Setup() {
            sut = new PriceCalculatorManager(orderRepository.Object, taxService.Object);
        }

        [Test]
        public void WithNoBogoDiscountShouldNotApplyAnyDiscount() {
            var orderListItem = new OrderLineItem(1, 1, 4.99, 10, false, null);
            Order order = new Order(new List<OrderLineItem> { orderListItem }, new ShippingAddress("123 St", "Zombie", "Ghost", "12345"));
            orderRepository.Setup(r => r.GetBy(1)).Returns(order);
            taxService.Setup(ps => ps.GetSalesTaxRate(order.ShippingAddress)).Returns(7);

            var subTotal = orderListItem.Price * orderListItem.Quantity;
            var actual = sut.GetTotalPriceOf(1);
            var expected = subTotal + subTotal * .07;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderWithMultipleLineItemsWithoutBogoDiscountShouldNotApplyAnyDiscount() {
            var orderListItem = new OrderLineItem(1, 1, 4.99, 10, false, null);
            var orderLineItem2 = new OrderLineItem(1, 2, 10.89, 2, false, null);
            Order order = new Order(new List<OrderLineItem> { orderListItem, orderLineItem2 }, new ShippingAddress("123 St", "Zombie", "Ghost", "12345"));
            orderRepository.Setup(r => r.GetBy(1)).Returns(order);
            taxService.Setup(ps => ps.GetSalesTaxRate(order.ShippingAddress)).Returns(7);

            var subTotal = (orderListItem.Price * orderListItem.Quantity) + (orderLineItem2.Price * orderLineItem2.Quantity);
            var actual = sut.GetTotalPriceOf(1);
            var expected = subTotal + subTotal * .07;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WithBogoDiscountNoLimitShouldApplyDiscountOnHalfOfQuantity() {
            var orderListItem = new OrderLineItem(1, 1, 4.99, 10, true, null);
            Order order = new Order(new List<OrderLineItem> { orderListItem }, new ShippingAddress("123 St", "Zombie", "Ghost", "12345"));
            orderRepository.Setup(r => r.GetBy(1)).Returns(order);
            taxService.Setup(ps => ps.GetSalesTaxRate(order.ShippingAddress)).Returns(7);

            var subTotal = (5 * orderListItem.Price / 2) + (5 * orderListItem.Price);
            var actual = sut.GetTotalPriceOf(1);
            var expected = subTotal + subTotal * .07;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void OrderWithMultipleLineItemsWithBogoDiscountNoLimitShouldApplyDiscountOnHalfOfQuantity() {
            var orderListItem = new OrderLineItem(1, 1, 4.99, 10, true, null);
            var orderLineItem2 = new OrderLineItem(1, 2, 10.89, 2, false, null);
            Order order = new Order(new List<OrderLineItem> { orderListItem, orderLineItem2 }, new ShippingAddress("123 St", "Zombie", "Ghost", "12345"));
            orderRepository.Setup(r => r.GetBy(1)).Returns(order);
            taxService.Setup(ps => ps.GetSalesTaxRate(order.ShippingAddress)).Returns(7);

            var subTotal = (5 * orderListItem.Price / 2) + (5 * orderListItem.Price) + (orderLineItem2.Price * orderLineItem2.Quantity);
            var actual = sut.GetTotalPriceOf(1);
            var expected = subTotal + subTotal * .07;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WithBogoDiscountWithMaxLimitAndHasExactQuantityAsMaxLimitShouldApplyDiscountOnHalfOfQuantity() {
            var orderListItem = new OrderLineItem(1, 1, 4.99, 10, true, 10);
            Order order = new Order(new List<OrderLineItem> { orderListItem }, new ShippingAddress("123 St", "Zombie", "Ghost", "12345"));
            orderRepository.Setup(r => r.GetBy(1)).Returns(order);
            taxService.Setup(ps => ps.GetSalesTaxRate(order.ShippingAddress)).Returns(7);

            var subTotal = (5 * orderListItem.Price / 2) + (5 * orderListItem.Price);
            var actual = sut.GetTotalPriceOf(1);
            var expected = subTotal + subTotal * .07;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WithBogoDiscountWithMaxLimitAndExceededMaxLimitShouldApplyRegularPriceForExceededQuantity() {
            var orderListItem = new OrderLineItem(1, 1, 4.99, 12, true, 10);
            Order order = new Order(new List<OrderLineItem> { orderListItem }, new ShippingAddress("123 St", "Zombie", "Ghost", "12345"));
            orderRepository.Setup(r => r.GetBy(1)).Returns(order);
            taxService.Setup(ps => ps.GetSalesTaxRate(order.ShippingAddress)).Returns(7);

            var subTotal = (5 * orderListItem.Price / 2) + (7 * orderListItem.Price);
            var actual = sut.GetTotalPriceOf(1);
            var expected = subTotal + subTotal * .07;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WithBogoDiscountWithMaxLimitAndHasLessQuantityAsMaxLimitShouldApplyDiscountOnHalfOfQuantity() {
            var orderListItem = new OrderLineItem(1, 1, 4.99, 9, true, 10);
            Order order = new Order(new List<OrderLineItem> { orderListItem }, new ShippingAddress("123 St", "Zombie", "Ghost", "12345"));
            orderRepository.Setup(r => r.GetBy(1)).Returns(order);
            taxService.Setup(ps => ps.GetSalesTaxRate(order.ShippingAddress)).Returns(7);

            var subTotal = (4 * orderListItem.Price / 2) + (5 * orderListItem.Price);
            var actual = sut.GetTotalPriceOf(1);
            var expected = subTotal + subTotal * .07;
            Assert.AreEqual(expected, actual);
        }
    }
}