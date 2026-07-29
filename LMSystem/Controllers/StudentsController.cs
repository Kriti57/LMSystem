using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Controllers
{
    public class StudentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentsController(ApplicationDbContext context) => _context = context;

        [Authorize]
        public async Task<IActionResult> Index(string search)
        {
            var query = _context.Students.AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(s => s.Name.Contains(search) || s.Email.Contains(search));
            return View(await query.ToListAsync());
        }

        [Authorize]
        public IActionResult Create() => View(new Student());

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Student student)
        {
            if (!ModelState.IsValid) return View(student);
            _context.Add(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return NotFound();
            return View(student);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Student student)
        {
            if (id != student.Id) return NotFound();
            if (!ModelState.IsValid) return View(student);
            _context.Update(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student != null)
            {
                _context.Students.Remove(student);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}