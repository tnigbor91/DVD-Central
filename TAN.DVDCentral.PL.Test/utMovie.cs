using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TAN.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovie
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
            Assert.AreEqual(3, dc.tblMovies.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            //Make entity
            tblMovie entity = new tblMovie();
            entity.Id = -99;
            entity.Title = "Test Title";
            entity.Description = "Test Data";
            entity.FormatId = 1;
            entity.DirectorId = 1;
            entity.RatingId = 1;
            entity.Cost = 999.99;
            entity.InStkQty = 999;
            entity.ImagePath = "Test/Path";

            //Add to DB
            dc.tblMovies.Add(entity);

            //Commit Changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //SELECT * FROM tblMovie -- Read all data and grab the first one
            tblMovie entity = dc.tblMovies.FirstOrDefault();

            //Change property values, save, and test
            entity.Title = "Test Title";
            entity.Description = "Test Data";
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblMovie WHERE Id = 3
            tblMovie entity = dc.tblMovies.Where(e => e.Id == 3).FirstOrDefault();

            dc.tblMovies.Remove(entity);
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblMovie entity = dc.tblMovies.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}
