using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TAN.DVDCentral.PL.Test
{
    [TestClass]
    public class utDirector
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
            Assert.AreEqual(3, dc.tblDirectors.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            //Make entity
            tblDirector entity = new tblDirector();
            entity.Id = -99;
            entity.FirstName = "Test";
            entity.LastName = "Test";


            //Add to DB
            dc.tblDirectors.Add(entity);

            //Commit Changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //SELECT * FROM tblDirector -- Read all data and grab the first one
            tblDirector entity = dc.tblDirectors.FirstOrDefault();

            //Change property values, save, and test
            entity.FirstName = "Test";
            entity.LastName = "Test";
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblDirector WHERE Id = 3
            tblDirector entity = dc.tblDirectors.Where(e => e.Id == 3).FirstOrDefault();

            dc.tblDirectors.Remove(entity);
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblDirector entity = dc.tblDirectors.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}
