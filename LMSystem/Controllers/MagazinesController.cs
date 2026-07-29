using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Controllers
{
    public class MagazinesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MagazinesController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string search, string status)
        {
            var query = _context.Magazines.AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(m => m.Title.Contains(search) || m.Publisher.Contains(search));
            if (status == "Available") query = query.Where(m => !m.IsBorrowed);
            if (status == "Borrowed") query = query.Where(m => m.IsBorrowed);
            return View(await query.ToListAsync());
        }

        [Authorize]
        public IActionResult Create() => View(new Magazine());

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Magazine magazine)
        {
            if (!ModelState.IsValid) return View(magazine);
            _context.Add(magazine);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var magazine = await _context.Magazines.FindAsync(id);
            if (magazine == null) return NotFound();
            return View(magazine);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Magazine magazine)
        {
            if (id != magazine.Id) return NotFound();
            if (!ModelState.IsValid) return View(magazine);
            var existing = await _context.Magazines.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Title = magazine.Title;
            existing.Publisher = magazine.Publisher;
            existing.Category = magazine.Category;
            existing.Issue = magazine.Issue;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var magazine = await _context.Magazines.FindAsync(id);
            if (magazine == null) return NotFound();
            return View(magazine);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var magazine = await _context.Magazines.FindAsync(id);
            if (magazine != null)
            {
                _context.Magazines.Remove(magazine);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Borrow(int id)
        {
            var magazine = await _context.Magazines.FindAsync(id);
            if (magazine != null && !magazine.IsBorrowed)
            {
                var user = await _userManager.GetUserAsync(User);
                magazine.IsBorrowed = true;
                magazine.BorrowedByUserId = user?.Id;
                magazine.BorrowedByEmail = user?.Email;
                magazine.BorrowDate = DateTime.Today;
                magazine.DueDate = DateTime.Today.AddDays(14);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Return(int id)
        {
            var magazine = await _context.Magazines.FindAsync(id);
            var currentUserId = _userManager.GetUserId(User);
            bool isAdmin = User.IsInRole("Administrator") || User.IsInRole("Librarian");

            if (magazine != null && magazine.IsBorrowed && (magazine.BorrowedByUserId == currentUserId || isAdmin))
            {
                magazine.IsBorrowed = false;
                magazine.BorrowedByUserId = null;
                magazine.BorrowedByEmail = null;
                magazine.BorrowDate = null;
                magazine.DueDate = null;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}