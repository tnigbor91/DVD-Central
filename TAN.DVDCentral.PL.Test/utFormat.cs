using System.Xml;

namespace TAN.DVDCentral.PL.Test
{
    [TestClass]
    public class utFormat
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
            Assert.AreEqual(3, dc.tblFormats.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            //Make entity
            tblFormat entity = new tblFormat();
            entity.Id = -99;
            entity.Description = "Test Data";

            //Add to DB
            dc.tblFormats.Add(entity);

            //Commit Changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //SELECT * FROM tblFormat -- Read all data and grab the first one
            tblFormat entity = dc.tblFormats.FirstOrDefault();

            //Change property values, save, and test
            entity.Description = "Test Data";
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblFormat WHERE Id = 3
            tblFormat entity = dc.tblFormats.Where(e => e.Id == 3).FirstOrDefault();

            dc.tblFormats.Remove(entity);
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblFormat entity = dc.tblFormats.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}