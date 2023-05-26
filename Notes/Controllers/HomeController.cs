using Microsoft.AspNetCore.Mvc;
using Notes.Data;
using Notes.Models;
using System.Diagnostics;
using System.Security.Claims;

namespace Notes.Controllers
{
    public class HomeController : Controller
    {
        private readonly NotesDbContext _dbContext;

        public HomeController(NotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AddNote()
        {
            return RedirectToAction("AddNote", "Note");
        }

        [HttpGet]
        public IActionResult Notes()
        {
            var notes = _dbContext
                .Notes
                .Where(x => x.UserId.Equals(User.FindFirstValue(ClaimTypes.NameIdentifier)))
                .ToList();

            return View(notes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}