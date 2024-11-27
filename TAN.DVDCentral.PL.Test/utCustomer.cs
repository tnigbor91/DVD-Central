using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TAN.DVDCentral.PL.Test
{
    [TestClass]
    public class utCustomer
    {
        protected DVDCentralEntities dc;
        protected IDbContextTransaction transaction;

        [TestInitialize]
        public void Initialize()
        {
            dc = new DVDCentralEntities();
            transaction = dc.Database.BeginTransaction();
        }

        [TestCleanup]
        public void Cleanup()
        {
            transaction.Rollback();
            transaction.Dispose();
            dc = null;
        }

        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, dc.tblCustomers.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            //Make entity
            tblCustomer entity = new tblCustomer();
            entity.Id = -99;
            entity.FirstName = "Test";
            entity.LastName = "Test";
            entity.UserId = -99;
            entity.Address = "123 Test Street";
            entity.City = "Testopolis";
            entity.State = "WI";
            entity.ZIP = "999999";
            entity.Phone = "9999999999";


            //Add to DB
            dc.tblCustomers.Add(entity);

            //Commit Changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //SELECT * FROM tblCustomer -- Read all data and grab the first one
            tblCustomer entity = dc.tblCustomers.FirstOrDefault();

            //Change property values, save, and test
            entity.FirstName = "Test";
            entity.LastName = "Test";
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblCustomer WHERE Id = 3
            tblCustomer entity = dc.tblCustomers.Where(e => e.Id == 3).FirstOrDefault();

            dc.tblCustomers.Remove(entity);
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblCustomer entity = dc.tblCustomers.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}
