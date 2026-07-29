using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Controllers
{
    public class NewspapersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public NewspapersController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string search, string status)
        {
            var query = _context.Newspapers.AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(n => n.Title.Contains(search) || n.Publisher.Contains(search));
            if (status == "Available") query = query.Where(n => !n.IsBorrowed);
            if (status == "Borrowed") query = query.Where(n => n.IsBorrowed);
            return View(await query.ToListAsync());
        }

        [Authorize]
        public IActionResult Create() => View(new Newspaper { Date = DateTime.Today });

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Newspaper newspaper)
        {
            if (!ModelState.IsValid) return View(newspaper);
            _context.Add(newspaper);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var newspaper = await _context.Newspapers.FindAsync(id);
            if (newspaper == null) return NotFound();
            return View(newspaper);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Newspaper newspaper)
        {
            if (id != newspaper.Id) return NotFound();
            if (!ModelState.IsValid) return View(newspaper);
            var existing = await _context.Newspapers.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Title = newspaper.Title;
            existing.Publisher = newspaper.Publisher;
            existing.Language = newspaper.Language;
            existing.Date = newspaper.Date;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var newspaper = await _context.Newspapers.FindAsync(id);
            if (newspaper == null) return NotFound();
            return View(newspaper);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var newspaper = await _context.Newspapers.FindAsync(id);
            if (newspaper != null)
            {
                _context.Newspapers.Remove(newspaper);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Borrow(int id)
        {
            var newspaper = await _context.Newspapers.FindAsync(id);
            if (newspaper != null && !newspaper.IsBorrowed)
            {
                var user = await _userManager.GetUserAsync(User);
                newspaper.IsBorrowed = true;
                newspaper.BorrowedByUserId = user?.Id;
                newspaper.BorrowedByEmail = user?.Email;
                newspaper.BorrowDate = DateTime.Today;
                newspaper.DueDate = DateTime.Today.AddDays(7);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Return(int id)
        {
            var newspaper = await _context.Newspapers.FindAsync(id);
            var currentUserId = _userManager.GetUserId(User);
            bool isAdmin = User.IsInRole("Administrator") || User.IsInRole("Librarian");

            if (newspaper != null && newspaper.IsBorrowed && (newspaper.BorrowedByUserId == currentUserId || isAdmin))
            {
                newspaper.IsBorrowed = false;
                newspaper.BorrowedByUserId = null;
                newspaper.BorrowedByEmail = null;
                newspaper.BorrowDate = null;
                newspaper.DueDate = null;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}