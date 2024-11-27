using TAN.DVDCentral.BL.Models;

namespace TAN.DVDCentral.BL.Test
{
    [TestClass]
    public class utOrder
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, OrderManager.Load().Count);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            int id = OrderManager.Load().LastOrDefault().Id;
            Order order = OrderManager.LoadById(id);
            Assert.AreEqual(order.Id, id);
            Assert.IsTrue(order.OrderItems.Count > 0);
        }

        [TestMethod]
        public void LoadByIdCustomerIdTest()
        {
            int customerId = OrderManager.Load().FirstOrDefault().CustomerId;
            Assert.AreEqual(OrderManager.LoadById(customerId).CustomerId, customerId);
        }

        [TestMethod]
        public void LoadByOrderIdTest()
        {
            int orderId = OrderItemManager.Load().FirstOrDefault().OrderId;
            Assert.IsTrue(OrderItemManager.LoadByOrderId(orderId).Count > 0);
        }

        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = OrderManager.Insert(-99, new DateTime (2010, 10, 10), -99, new DateTime(2010, 10, 10), new List<OrderItem>(), ref id, true);
            Assert.AreEqual(4, id);
            Assert.AreEqual(1, results);
        }


        [TestMethod]
        public void UpdateTest()
        {
            Order order = OrderManager.LoadById(1);
            order.UserId = -99;
            int results = OrderManager.Update(order, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = OrderManager.Delete(1, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void InsertOrderItemsTest()
        {
            Order order = new Order
            {
                CustomerId = 99,
                OrderDate = DateTime.Now,
                UserId = 99,
                ShipDate = DateTime.Now,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        Id = 88,
                        MovieId = 99,
                        Cost = 999.99,
                        Quantity = 99
                    },
                    new OrderItem
                    {
                        Id = 99,
                        MovieId = 2,
                        Cost = 99.11,
                        Quantity = 100
                    }
                }
            };
            int result = OrderManager.Insert(order, true);
            Assert.AreEqual(4, order.Id);
            Assert.AreEqual(3, result);
        }
    }
}