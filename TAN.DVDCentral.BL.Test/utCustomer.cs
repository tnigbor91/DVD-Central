using TAN.DVDCentral.BL.Models;

namespace TAN.DVDCentral.BL.Test
{
    [TestClass]
    public class utCustomer
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, CustomerManager.Load().Count);
        }
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = CustomerManager.Insert("Test", "Test", -99, "Test St.", "Testville", "TN", "55555", "5555555555",  ref id, true);
            Assert.AreEqual(4, id);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Customer customer = new Customer
            {
                FirstName = "Test",
                LastName = "Test",
                UserId = -99,
                Address = "Test Ave",
                City = "Test Town",
                State = "MN",
                Zip = "999999",
                Phone = "9202225555"
                
            };
            int results = CustomerManager.Insert(customer, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Customer customer = CustomerManager.LoadById(1);
            customer.LastName = "Test";
            int results = CustomerManager.Update(customer, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = CustomerManager.Delete(1, true);
            Assert.AreEqual(1, results);
        }
    }
}