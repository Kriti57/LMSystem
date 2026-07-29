using LMSystem.Data;
using LMSystem.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace LMSystem.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            ApplicationDbContext context,
            UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Members go directly to their borrowed books
            if (User.IsInRole("Member"))
            {
                return RedirectToAction("Index", "MyBorrows");
            }

            var totalTitles =
                await _context.Books.CountAsync()
                + await _context.Magazines.CountAsync()
                + await _context.Newspapers.CountAsync();

            var currentlyBorrowed =
                await _context.Books.CountAsync(b => b.IsBorrowed)
                + await _context.Magazines.CountAsync(m => m.IsBorrowed)
                + await _context.Newspapers.CountAsync(n => n.IsBorrowed);

            var registeredMembers =
                (await _userManager.GetUsersInRoleAsync("Member")).Count;

            var model = new HomeStatsViewModel
            {
                TotalTitles = totalTitles,
                CurrentlyBorrowed = currentlyBorrowed,
                RegisteredStudents = registeredMembers
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}