using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public BooksController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index(string search, string status)
        {
            var query = _context.Books.AsQueryable();
            if (!string.IsNullOrEmpty(search))
                query = query.Where(b => b.Title.Contains(search) || b.Author.Contains(search));
            if (status == "Available") query = query.Where(b => !b.IsBorrowed);
            if (status == "Borrowed") query = query.Where(b => b.IsBorrowed);
            return View(await query.ToListAsync());
        }

        [Authorize]
        public IActionResult Create() => View(new Book());

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (!ModelState.IsValid) return View(book);
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id) return NotFound();
            if (!ModelState.IsValid) return View(book);
            var existing = await _context.Books.FindAsync(id);
            if (existing == null) return NotFound();
            existing.Title = book.Title;
            existing.Author = book.Author;
            existing.Genre = book.Genre;
            existing.PublicationYear = book.PublicationYear;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null) return NotFound();
            return View(book);
        }

        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Member")]
        public async Task<IActionResult> Borrow(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null && !book.IsBorrowed)
            {
                var user = await _userManager.GetUserAsync(User);
                book.IsBorrowed = true;
                book.BorrowedByUserId = user?.Id;
                book.BorrowedByEmail = user?.Email;
                book.BorrowDate = DateTime.Today;
                book.DueDate = DateTime.Today.AddDays(14);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize]
        public async Task<IActionResult> Return(int id)
        {
            var book = await _context.Books.FindAsync(id);
            var currentUserId = _userManager.GetUserId(User);
            bool isAdmin = User.IsInRole("Administrator") || User.IsInRole("Librarian");

            if (book != null && book.IsBorrowed && (book.BorrowedByUserId == currentUserId || isAdmin))
            {
                book.IsBorrowed = false;
                book.BorrowedByUserId = null;
                book.BorrowedByEmail = null;
                book.BorrowDate = null;
                book.DueDate = null;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}