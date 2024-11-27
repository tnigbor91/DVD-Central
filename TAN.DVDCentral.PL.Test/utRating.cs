using System.Xml;

namespace TAN.DVDCentral.PL.Test
{
    [TestClass]
    public class utRating
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
            Assert.AreEqual(3, dc.tblRatings.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            //Make entity
            tblRating entity = new tblRating();
            entity.Id = -99;
            entity.Description = "Test Data";

            //Add to DB
            dc.tblRatings.Add(entity);

            //Commit Changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //SELECT * FROM tblRating -- Read all data and grab the first one
            tblRating entity = dc.tblRatings.FirstOrDefault();

            //Change property values, save, and test
            entity.Description = "Test Data";
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblRating WHERE Id = 3
            tblRating entity = dc.tblRatings.Where(e => e.Id == 3).FirstOrDefault();

            dc.tblRatings.Remove(entity);
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblRating entity = dc.tblRatings.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}