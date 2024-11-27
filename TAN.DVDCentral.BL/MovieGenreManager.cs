using Microsoft.CodeAnalysis.FlowAnalysis.DataFlow;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TAN.DVDCentral.BL.Models;
using TAN.DVDCentral.PL;

namespace TAN.DVDCentral.BL
{
    public class MovieGenreManager
    {
        public static int Insert(int movieId, int genreId, ref int id, bool rollback = false)
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback)
                    {
                        transaction = dc.Database.BeginTransaction();
                        tblMovieGenre row = new tblMovieGenre();

                        id = row.Id = dc.tblMovieGenres.Any() ? dc.tblMovieGenres.Max(r => r.Id) + 1 : 1;
                        row.MovieId = movieId;
                        row.GenreId = genreId;

                        dc.tblMovieGenres.Add(row);

                        results = dc.SaveChanges();
                        if(rollback) transaction.Rollback();
                    }
                }
                return results;

            }
            catch (Exception)
            {

                throw;
            }
        }
        public static int Delete(int movieId, int genreId, bool rollback= false) 
        {
            try
            {
                int results = 0;
                using (DVDCentralEntities dc = new DVDCentralEntities())
                {
                    IDbContextTransaction transaction = null;
                    if (rollback)
                    {
                        transaction = dc.Database.BeginTransaction();
                        tblMovieGenre row = dc.tblMovieGenres.FirstOrDefault(r => r.MovieId == movieId && r.GenreId == genreId);

                        if (row != null)
                        {
                            dc.tblMovieGenres.Remove(row);
                            results = dc.SaveChanges();
                            if (rollback) transaction.Rollback();
                        }
                        else
                        {
                            throw new Exception("Row does not exist");
                        }
                    }
                }
                return results;

            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
