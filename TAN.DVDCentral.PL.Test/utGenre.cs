using System.Xml;

namespace TAN.DVDCentral.PL.Test
{
    [TestClass]
    public class utGenre
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
            Assert.AreEqual(3, dc.tblGenres.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            //Make entity
            tblGenre entity = new tblGenre();
            entity.Id = -99;
            entity.Description = "Test Data";

            //Add to DB
            dc.tblGenres.Add(entity);

            //Commit Changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //SELECT * FROM tblGenre -- Read all data and grab the first one
            tblGenre entity = dc.tblGenres.FirstOrDefault();

            //Change property values, save, and test
            entity.Description = "Test Data";
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblGenre WHERE Id = 3
            tblGenre entity = dc.tblGenres.Where(e => e.Id == 3).FirstOrDefault();

            dc.tblGenres.Remove(entity);
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblGenre entity = dc.tblGenres.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}