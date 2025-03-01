using TAN.DVDCentral.BL.Models;

namespace TAN.DVDCentral.BL.Test
{
    [TestClass]
    public class utGenre
    {
        [TestMethod]
        public void LoadTest()
        {
            Assert.AreEqual(3, GenreManager.Load().Count);
        }
        [TestMethod]
        public void InsertTest1()
        {
            int id = 0;
            int results = GenreManager.Insert("Test", ref id, true);
            Assert.AreEqual(4, id);
            Assert.AreEqual(1, results);
        }
        [TestMethod]
        public void InsertTest2()
        {
            int id = 0;
            Genre genre = new Genre
            {
                Description = "Test"
            };
            int results = GenreManager.Insert(genre, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void UpdateTest()
        {
            Genre genre = GenreManager.LoadById(1);
            genre.Description = "Test";
            int results = GenreManager.Update(genre, true);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int results = GenreManager.Delete(1, true);
            Assert.AreEqual(1, results);
        }
    }
}