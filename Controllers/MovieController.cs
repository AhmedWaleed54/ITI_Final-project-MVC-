using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Models;

namespace Project.Controllers
{
    public class MovieController : Controller
    {
        ApplicationDbContext _context;
        IWebHostEnvironment _environment;
        public MovieController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _environment = environment;
            _context = context;
        }
        [HttpGet]

        public IActionResult GetIndexView(string? search)
        {
            ViewBag.Search = search;
            if (string.IsNullOrEmpty(search) == true)
            {
                return View("Index", _context.Movie.ToList());
            }
            else
            {
                return View("Index", _context.Movie.Where(m =>
                m.MovieTitle.Contains(search) || m.MovieDescription.Contains(search) || m.MovieType.Contains(search)).ToList());
            }
        }
        [HttpGet]
        public IActionResult GetFormToAdd()
        {
            return View("CreateMovie");
        }
        [HttpPost]
        public IActionResult AddNew(Movie movie, IFormFile? imageFormFile)
        {
            if (imageFormFile != null)
            {
                string imgExtention = Path.GetExtension(imageFormFile.FileName);
                Guid imgGuid = Guid.NewGuid();
                string imgName = imgGuid + imgExtention;
                string imgPath = "\\images\\" + imgName;
                movie.ImagePath = imgPath;
                string imgFullPath = _environment.WebRootPath + imgPath;
                FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                imageFormFile.CopyTo(imgFileStream);
                imgFileStream.Dispose();
            }
            else
            {
                movie.ImagePath = "\\images\\No_Image.png";
            }
            
            if (ModelState.IsValid)
            {
                _context.Movie.Add(movie);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("CreateMovie", movie);
            }
        }

        public IActionResult GetEditView(int ID)
        {
            Movie mov = _context.Movie.FirstOrDefault(d => d.MovieId == ID);
            if (mov == null)
            {
                return NotFound();
            }
            else
            {
                return View("Edit",mov);
            }


        }
        [HttpPost]
        public IActionResult EditCurrent(Movie mov, IFormFile? imageFormFile)
        {
            
            if (ModelState.IsValid == true)
            {
                if (imageFormFile != null)
                {
                    if (mov.ImagePath != "\\images\\No_Image.png")
                    {
                        string oldImgFullPath = _environment.WebRootPath + mov.ImagePath;
                        System.IO.File.Delete(oldImgFullPath);
                    }
                    string imgExtension = Path.GetExtension(imageFormFile.FileName);
                    Guid imgGuid = Guid.NewGuid();
                    string imgName = imgGuid + imgExtension;
                    string imgPath = "\\images\\" + imgName;
                    mov.ImagePath = imgPath;
                    string imgFullPath = _environment.WebRootPath + imgPath;

                    FileStream imgFileStream = new FileStream(imgFullPath, FileMode.Create);
                    imageFormFile.CopyTo(imgFileStream);
                    imgFileStream.Close();
                }


                _context.Movie.Update(mov);
                _context.SaveChanges();
                return RedirectToAction("GetIndexView");
            }
            else
            {
                return View("Edit");
            }

        }

        public IActionResult GetDetailsView(int id)
        {
            Movie mov = _context.Movie.Include(d => d.rating).FirstOrDefault(em => em.MovieId == id);
            ViewBag.CurrentData = mov;

            if (mov == null)
                return NotFound();
            return View("Details", mov);
        }
        [HttpGet]
        public IActionResult GetDeleteView(int id)
        {
            Movie mov = _context.Movie.Include(d => d.rating).FirstOrDefault(em => em.MovieId == id);
            ViewBag.CurrentData = mov;
            if (mov == null)
                return NotFound();
            return View("Delete", mov);
        }


        [HttpPost]
        public IActionResult DeleteCurrent(int movieId)
        {

            Movie mov = _context.Movie.Find(movieId);


            if (mov != null && mov.ImagePath != "\\images\\No_Image.png")
            {
                string imgFullPath = _environment.WebRootPath + mov.ImagePath;
                System.IO.File.Delete(imgFullPath);
            }

            _context.Movie.Remove(mov);
            _context.SaveChanges();
            return RedirectToAction("GetIndexView");

        }
    }
}
