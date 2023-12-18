using CsvHelper;
using CsvHelper.Configuration;
using CSVReadWrite.Interface;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.ProgramSynthesis.Common.OpenAI;
using NPOI.SS.Formula.Functions;
using System.Globalization;
using System.IO;
using System.Text;
using static Microsoft.ProgramSynthesis.Diagnostics.Location;

namespace A4_MovieLibrary
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var choice = "";
            do
            {
                Console.WriteLine("1. List all movies");
                Console.WriteLine("2. Add a movie");
                Console.WriteLine("3. Exit");

                Console.WriteLine("Enter your choice :");
                choice = Console.ReadLine();

                if (choice == "1")
                {
                    ListAllMovies();
                }
                else if (choice == "2")
                {
                    AddMovie();
                }
                 
            } while (choice != "3");

          
        }
        static void ListAllMovies()
        {
            using (var reader = new StreamReader("C:\\Users\\Eduardo\\source\\repos\\A4-MovieLibrary\\A4-MovieLibrary\\A4-MovieLibrary\\Files\\movies.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var movies = csv.GetRecords<Movie>().ToList();

                if (movies.Any())
                {
                    Console.WriteLine("MovieId\tTitle\t\tGenres");
                    foreach (var movie in movies)
                    {
                        Console.WriteLine($"{movie.movieId}\t{movie.title}\t{movie.genres}");
                    }
                }
                else
                {
                    Console.WriteLine("No movies found.");
                }
            }
        }
        static void AddMovie()
        {
            // enter movie title into csvReader
            Console.WriteLine("Enter movie title:");
            string title = Console.ReadLine();

            using (var reader = new StreamReader("C:\\Users\\Eduardo\\source\\repos\\A4_MovieLibrary\\A4-MovieLibrary\\A4-MovieLibrary\\Files\\movies.csv"))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var movies = csv.GetRecords<Movie>().ToList();
                // this checks for duplicate titles
                if (movies.Any(m => m.title.Equals(title, StringComparison.OrdinalIgnoreCase)))
                {
                    Console.WriteLine("Movie has same title as one that exists");
                }
                //the next part checks to verify what is the next movie id 
                int nextMovieId = movies.Count > 0 ? movies.Max(m => m.movieId) + 1 : 1;

                Console.Write("Enter the movie Genres: ");
                string movieGenre = Console.ReadLine();

                movies.Add(new Movie { movieId = nextMovieId, title = title, genres = movieGenre });
            }


        }
        

    }
} 