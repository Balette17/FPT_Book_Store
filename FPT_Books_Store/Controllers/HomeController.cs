using FPT_Books_Store.Data;
using FPT_Books_Store.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FPT_Books_Store.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> IndexAsync()
        {
            var applicationDbContext = _context.Book.Include(b => b.Author).Include(b => b.Category).Include(b => b.PublishingCompany);
            return View(await applicationDbContext.ToListAsync());
          
        }

        [HttpGet]
        public async Task<IActionResult> Search(string searchTerm)
        {
            // Tìm kiếm sách trong cơ sở dữ liệu dựa trên searchTerm
            var searchResults = await _context.Book
                .Where(book => book.Name.Contains(searchTerm) || book.Description.Contains(searchTerm))
                .ToListAsync();

            // Trả về kết quả tìm kiếm
            return View(searchResults);
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}