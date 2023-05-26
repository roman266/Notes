using Microsoft.AspNetCore.Mvc;
using Notes.Data;
using Notes.Models;
using System.Security.Claims;

namespace Notes.Controllers
{
    public class NoteController : Controller
    {
        private readonly NotesDbContext _dbContext;

        public NoteController(NotesDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult AddNote()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddNote(NoteModel entity)
        {
            entity.CreateDate = DateTime.UtcNow;
            entity.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            _dbContext.Add(entity);
            _dbContext.SaveChanges();

            return RedirectToAction("Notes", "Home");
        }

        [HttpPost]
        public IActionResult DeleteNote(int id)
        {
            var entity = _dbContext
                .Notes
                .First(x => x.Id.Equals(id) && x.UserId.Equals(User.FindFirstValue(ClaimTypes.NameIdentifier)));

            _dbContext.Notes.Remove(entity);
            _dbContext.SaveChanges();

            return RedirectToAction("Notes", "Home");
        }
    }
}
