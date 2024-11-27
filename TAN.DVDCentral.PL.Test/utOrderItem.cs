using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TAN.DVDCentral.PL.Test
{
    [TestClass]
    public class utOrderItem
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
            Assert.AreEqual(3, dc.tblOrderItems.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            //Make entity
            tblOrderItem entity = new tblOrderItem();
            entity.Id = -99;
            entity.OrderId = -99;
            entity.Quantity = 99;
            entity.MovieId = -99;
            entity.Cost = 999.99;


            //Add to DB
            dc.tblOrderItems.Add(entity);

            //Commit Changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //SELECT * FROM tblOrderItem -- Read all data and grab the first one
            tblOrderItem entity = dc.tblOrderItems.FirstOrDefault();

            //Change property values, save, and test
            entity.Quantity = 999;
            entity.Cost = 999;
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblOrderItem WHERE Id = 3
            tblOrderItem entity = dc.tblOrderItems.Where(e => e.Id == 3).FirstOrDefault();

            dc.tblOrderItems.Remove(entity);
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblOrderItem entity = dc.tblOrderItems.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}
