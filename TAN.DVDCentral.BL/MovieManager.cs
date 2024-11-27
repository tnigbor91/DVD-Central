using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAN.DVDCentral.BL.Models;
using TAN.DVDCentral.PL;

namespace TAN.DVDCentral.BL
{
    public class MovieManager
    {
        public static int Insert(string title, string description, int formatId,
                                 int directorId, int ratingId, double cost, int inStkQty, string imagePath,
                                 ref int id, bool rollback = false)
        {
            try
            {
                Movie movie = new Movie
                {
                    Title = title,
                    Description = description,
                    FormatId = formatId,
                    DirectorId = directorId,
                    RatingId = ratingId,
                    Cost = cost,
                    InStkQty = inStkQty,
                    ImagePath = imagePath
                };

                int results = Insert(movie, rollback);

                //Backfill
                id = movie.Id;
                return results;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public static int Insert(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    tblMovie entity = new tblMovie();
                    entity.Id = dc.tblMovies.Any() ? dc.tblMovies.Max(m => m.Id) + 1 : 1;
                    entity.Title = movie.Title;
                    entity.Description = movie.Description;
                    entity.FormatId = movie.FormatId;
                    entity.DirectorId = movie.DirectorId;
                    entity.RatingId = movie.RatingId;
                    entity.Cost = movie.Cost;
                    entity.InStkQty = movie.InStkQty;
                    entity.ImagePath = movie.ImagePath;

                    //Back fill
                    movie.Id = entity.Id;

                    dc.tblMovies.Add(entity);
                    results = dc.SaveChanges();

                    if (rollback) transaction.Rollback();

                }

                return results;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public static int Update(Movie movie, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get the row that we are trying to update
                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == movie.Id);

                    if (entity != null)
                    {
                        entity.Title = movie.Title;
                        entity.Description = movie.Description;
                        entity.FormatId = movie.FormatId;
                        entity.DirectorId = movie.DirectorId;
                        entity.RatingId = movie.RatingId;
                        entity.Cost = movie.Cost;
                        entity.InStkQty = movie.InStkQty;
                        entity.ImagePath = movie.ImagePath;

                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Delete(int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback) transaction = dc.Database.BeginTransaction();

                    // Get row
                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == id);

                    if (entity != null)
                    {
                        dc.tblMovies.Remove(entity);
                        results = dc.SaveChanges();
                    }
                    else
                    {
                        throw new Exception("Row does not exist");
                    }

                    if (rollback) transaction.Rollback();
                }
                return results;
            }
            catch (Exception)
            {

                throw;
            }
        }

        //Edit This
        public static Movie LoadById(int id)
        {
            try
            {
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    tblMovie entity = dc.tblMovies.FirstOrDefault(m => m.Id == id);

                    if (entity != null)
                    {
                        return new Movie
                        {
                            Id = entity.Id,
                            Title = entity.Title, 
                            Description = entity.Description,
                            FormatId = entity.FormatId,
                            DirectorId = entity.DirectorId,
                            RatingId = entity.RatingId,
                            Cost = entity.Cost,
                            InStkQty = entity.InStkQty,
                            ImagePath = entity.ImagePath
                        };
                    }
                    else
                    {
                        throw new Exception();
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public static List<Movie> Load(int? genreId = null)
        {
            try
            {
                List<Movie> list = new List<Movie>();

                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    (from m in dc.tblMovies
                     join f in dc.tblFormats on m.FormatId equals f.Id
                     join d in dc.tblDirectors on m.DirectorId equals d.Id
                     join r in dc.tblRatings on m.RatingId equals r.Id
                     where m.Id == genreId || genreId == null
                     select new
                     {
                         m.Id,
                         m.Title,
                         m.Description,
                         m.FormatId,
                         Format = f.Description,
                         m.DirectorId,
                         DirectorName = d.FirstName + " " + d.LastName,
                         m.RatingId,
                         Rating = r.Description,
                         m.Cost,
                         m.InStkQty,
                         m.ImagePath
                     })
                     .ToList()

                    .ForEach(movie => list.Add(new Models.Movie
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        Description = movie.Description,
                        FormatId = movie.FormatId,
                        Format = movie.Format,
                        DirectorId = movie.DirectorId,
                        DirectorName = movie.DirectorName,
                        RatingId = movie.RatingId,
                        Rating = movie.Rating,
                        Cost = movie.Cost,
                        InStkQty = movie.InStkQty,
                        ImagePath = movie.ImagePath
                    }));
                }

                return list;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
