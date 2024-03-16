using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class RatingController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _environment;
        public RatingController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment; 

        }
        public IActionResult GetIndexView(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {
                ViewBag.AllMovies = _context.Movie.ToList();
                return View("Index", _context.Rating.ToList());
            }
            else
            {
                ViewBag.AllMovies = _context.Movie.ToList();
                return View("Index", _context.Rating.Where(m =>
                m.ViewerName.Contains(search) || m.RatingDescription.Contains(search) || m.movie.MovieTitle.Contains(search)));
            }
        }
        public IActionResult GetFormToAdd()
        {
            ViewBag.AllMovies = _context.Movie.ToList();
            return View("CreateRating");
        }

        public IActionResult AddNew(Rating rating)
        {
            if (ModelState.IsValid)
            {
                _context.Rating.Add(rating);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                ViewBag.AllMovies = _context.Movie.ToList();
                return View("CreateRating", rating);
            }
        }

        public IActionResult GetDeleteView(int id)
        {
            Rating rating = _context.Rating.Include(d => d.movie).FirstOrDefault(em => em.RatingId == id);
            ViewBag.CurrentData = rating;
            if (rating == null)
                return NotFound();
            return View("Delete", rating);
        }

        public IActionResult GetDetailsView(int id)
        {
            Rating rating = _context.Rating.Include(d => d.movie).FirstOrDefault(em => em.RatingId == id);
            ViewBag.CurrentData = rating;

            if (rating == null)
                return NotFound();
            return View("Details", rating);
        }

        public IActionResult GetEditView(int ID)
        {
            Rating rating = _context.Rating.FirstOrDefault(d => d.RatingId == ID);
            if (rating == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit",rating);
            }


        }

        public IActionResult DeleteCurrent(int ratingId)
        {
            Rating rating = _context.Rating.Find(ratingId);

            //if (rating != null && rating.ImagePath != "\\images\\No_Image.png")
            //{
            //    string imgFullPath = _environment.WebRootPath + movie.ImagePath;
            //    System.IO.File.Delete(imgFullPath);
            //}

            _context.Rating.Remove(rating);
            _context.SaveChanges();

            return RedirectToAction("GetIndexView");


        }


    }
}
