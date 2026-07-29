using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LMSystem.Controllers
{
    [Authorize(Roles = "Member")]
    public class MyBorrowsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public MyBorrowsController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);

            var model = new MyBorrowsViewModel
            {
                Books = await _context.Books.Where(b => b.BorrowedByUserId == userId).ToListAsync(),
                Magazines = await _context.Magazines.Where(m => m.BorrowedByUserId == userId).ToListAsync(),
                Newspapers = await _context.Newspapers.Where(n => n.BorrowedByUserId == userId).ToListAsync()
            };

            return View(model);
        }
    }
}