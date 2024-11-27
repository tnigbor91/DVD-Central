using TAN.DVDCentral.BL.Models;
using static System.Net.Mime.MediaTypeNames;

namespace TAN.DVDCentral.BL.Test
{
    [TestClass]
    public class utMovieGenre
    {
        [TestMethod]
        public void InsertTest()
        {
            int id = 0;
            int genreId = 2;
            int movieId = 3;    

            int results = MovieGenreManager.Insert(movieId, genreId, ref id, true);

            Assert.AreEqual(4, id);
            Assert.AreEqual(1, results);
        }

        [TestMethod]
        public void DeleteTest()
        {
            int id = 1;
            int result = MovieGenreManager.Delete(2, 3, true);
            Assert.AreEqual(1, result);
        }
    }
}