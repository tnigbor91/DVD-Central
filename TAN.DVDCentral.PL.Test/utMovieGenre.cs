using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TAN.DVDCentral.PL.Test
{
    [TestClass]
    public class utMovieGenre
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
            Assert.AreEqual(3, dc.tblMovieGenres.Count());
        }

        [TestMethod]
        public void InsertTest()
        {
            //Make entity
            tblMovieGenre entity = new tblMovieGenre();
            entity.Id = -99;
            entity.GenreId = -99;
            entity.MovieId = -99;

            //Add to DB
            dc.tblMovieGenres.Add(entity);

            //Commit Changes
            int result = dc.SaveChanges();
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void UpdateTest()
        {
            //SELECT * FROM tblMovieGenre -- Read all data and grab the first one
            tblMovieGenre entity = dc.tblMovieGenres.FirstOrDefault();

            //Change property values, save, and test
            entity.GenreId = -99;
            entity.MovieId = -99;
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void DeleteTest()
        {
            // SELECT * FROM tblMovieGenre WHERE Id = 3
            tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.Id == 3).FirstOrDefault();

            dc.tblMovieGenres.Remove(entity);
            int result = dc.SaveChanges();
            Assert.IsTrue(result > 0);
        }

        [TestMethod]
        public void LoadByIdTest()
        {
            tblMovieGenre entity = dc.tblMovieGenres.Where(e => e.Id == 2).FirstOrDefault();
            Assert.AreEqual(entity.Id, 2);
        }
    }
}
