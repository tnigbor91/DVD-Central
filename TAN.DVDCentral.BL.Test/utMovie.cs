using TAN.DVDCentral.BL.Models;

namespace TAN.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovie
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, MovieManager.Load().Count);
        }
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = MovieManager.Insert("Test", "Test", 1, 1, 1, 999.99, 99, "TestPath", ref id, true);
            Assert.AreEqual(4, id);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Movie movie = new Movie
            {
                Title = "Test",
                Description = "Test",
                FormatId = 1,
                DirectorId = 1,
                RatingId = 1,
                Cost = 999.99,
                InStkQty = 99,
                ImagePath = "TestPath"
            };
            int results = MovieManager.Insert(movie, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Movie movie = MovieManager.LoadById(1);
            movie.Description = "Test";
            int results = MovieManager.Update(movie, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = MovieManager.Delete(1, true);
            Assert.AreEqual(1, results);
        }
    }
}