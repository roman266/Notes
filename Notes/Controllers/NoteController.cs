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

        private NoteModel GetEntityByIdForUser(int id) => _dbContext
            .Notes
            .First(x => x.Id.Equals(id) && x.UserId.Equals(User.FindFirstValue(ClaimTypes.NameIdentifier)));

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
            var entity = GetEntityByIdForUser(id);

            _dbContext.Notes.Remove(entity);
            _dbContext.SaveChanges();

            return RedirectToAction("Notes", "Home");
        }

        [HttpGet]
        public IActionResult UpdateNote(int id)
        {
            var entity = GetEntityByIdForUser(id);

            return View(entity);
        }

        [HttpPost]
        public IActionResult UpdateNote(NoteModel entity)
        {
            var currentEntity = GetEntityByIdForUser(entity.Id);

            currentEntity.Title = entity.Title;
            currentEntity.Description = entity.Description;
            currentEntity.Priority = entity.Priority;

            _dbContext.SaveChanges();

            return RedirectToAction("Notes", "Home");
        }
    }
}
