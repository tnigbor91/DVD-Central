using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TAN.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrder
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
            Assert.AreEqual(3, dc.tblOrders.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            //Make entity
            tblOrder entity = new tblOrder();
            entity.Id = -99;
            entity.CustomerId = -99;
            entity.OrderDate = new DateTime(2020, 12, 12);
            entity.UserId = -99;
            entity.ShipDate = new DateTime(2020, 12, 12);


            //Add to DB
            dc.tblOrders.Add(entity);

            //Commit Changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //SELECT * FROM tblOrder -- Read all data and grab the first one
            tblOrder entity = dc.tblOrders.FirstOrDefault();

            //Change property values, save, and test
            entity.UserId = -99;
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblOrder WHERE Id = 3
            tblOrder entity = dc.tblOrders.Where(e => e.Id == 3).FirstOrDefault();

            dc.tblOrders.Remove(entity);
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblOrder entity = dc.tblOrders.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}
