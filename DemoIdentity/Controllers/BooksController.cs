using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FPTBook.Models;
using FPTBookStore.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using FPTBook.Utils;
using System.Security.Claims;

namespace FPTBook.Controllers
{
    
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public BooksController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        [Authorize(Roles = "Owner")]

        // GET: Books
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Book.Include(b => b.Author).Include(b => b.Category).Include(b => b.PublishingCompany);
            return View(await applicationDbContext.ToListAsync());
        }
        
        // GET: Books/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.PublishingCompany)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "Id", "Name");
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "Id", "Name");
            ViewData["PublishingCompanyID"] = new SelectList(_context.Set<PublishingCompany>(), "Id", "Name");
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,CategoryID,Price,AuthorID,PublishingCompanyID,Quantity,Description")] Book book, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string fileName = "";
                string extension = "";
                if (file != null && file.Length > 0)
                {
                    string wwwRootPath = _hostEnvironment.WebRootPath;
                    fileName = Path.GetFileNameWithoutExtension(file.FileName);
                    extension = Path.GetExtension(file.FileName);
                    fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;

                    string path = Path.Combine(wwwRootPath, "Image", fileName);

                    using (var fileStream = new FileStream(path, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
                book.ImgFileName = fileName;
                book.ImgFileExt = extension;
                _context.Add(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "Id", "Name", book.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "Id", "Id", book.CategoryID);
            ViewData["PublishingCompanyID"] = new SelectList(_context.Set<PublishingCompany>(), "Id", "Name", book.PublishingCompanyID);
            return View(book);
        }

        // GET: Books/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "Id", "Name", book.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "Id", "Name", book.CategoryID);
            ViewData["PublishingCompanyID"] = new SelectList(_context.Set<PublishingCompany>(), "Id", "Name", book.PublishingCompanyID);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CategoryID,Price,AuthorID,PublishingCompanyID,Quantity,Description,ImgFileName,ImgFileExt")] Book book)
        {
            if (id != book.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(book);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookExists(book.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AuthorID"] = new SelectList(_context.Set<Author>(), "Id", "Name", book.AuthorID);
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "Id", "Name", book.CategoryID);
            ViewData["PublishingCompanyID"] = new SelectList(_context.Set<PublishingCompany>(), "Id", "Name", book.PublishingCompanyID);
            return View(book);
        }

        // GET: Books/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Book == null)
            {
                return NotFound();
            }

            var book = await _context.Book
                .Include(b => b.Author)
                .Include(b => b.Category)
                .Include(b => b.PublishingCompany)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Book == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Book'  is null.");
            }
            var book = await _context.Book.FindAsync(id);
            if (book != null)
            {
                _context.Book.Remove(book);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookExists(int id)
        {
          return (_context.Book?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        [HttpPost]
        public IActionResult Apptocart(int id, string name, decimal price, int quantity)
        {
            ShoppingCart myCart;
            // If the cart is not in the session, create one and put it there
            // Otherwise, get it from the session
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") == null)
            {
                myCart = new ShoppingCart();
                HttpContext.Session.SetObject("cart", myCart);
            }
            myCart = (ShoppingCart)HttpContext.Session.GetObject<ShoppingCart>("cart");
            var newItem = myCart.AddItem(id, name, price, quantity);
            HttpContext.Session.SetObject("cart", myCart);
            ViewData["newItem"] = newItem;
            return View();
        }
        public IActionResult CheckOut()
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") == null)
            {
                ShoppingCart myCart = new ShoppingCart();
                HttpContext.Session.SetObject("cart", myCart);
            }
            ShoppingCart cart = (ShoppingCart)HttpContext.Session.GetObject<ShoppingCart>("cart");
            ViewData["myItems"] = cart.Items;
            return View();
        }
        public IActionResult PlaceOrder(decimal total)
        {
            if (HttpContext.Session.GetObject<ShoppingCart>("cart") == null)
            {
                ShoppingCart myCart = new ShoppingCart();
                HttpContext.Session.SetObject("cart", myCart);
            }
            ShoppingCart cart = (ShoppingCart)HttpContext.Session.GetObject<ShoppingCart>("cart");
            Order myOrder = new Order();
            myOrder.UserID = User.FindFirstValue(ClaimTypes.NameIdentifier);
            myOrder.OrderTime = DateTime.Now;
            myOrder.Total = total;
            _context.Order.Add(myOrder);
            _context.SaveChanges();//this generates the Id for Order

            foreach (var item in cart.Items)
            {
                OrderItem myOrderItem = new OrderItem();
                myOrderItem.BooksID = item.ID;
                myOrderItem.Quantity = item.Quantity;
                myOrderItem.OrderID = myOrder.Id;//id of saved order above

                _context.OrderItem.Add(myOrderItem);
            }
            _context.SaveChanges();
            //empty shopping cart
            cart = new ShoppingCart();
            HttpContext.Session.SetObject("cart", cart);

            return RedirectToAction("CheckOut", "Books");
        }

        [HttpPost]
        public RedirectToActionResult EditOrder(int id, int quantity)
        {
            ShoppingCart cart = (ShoppingCart)HttpContext.Session.GetObject<ShoppingCart>("cart");
            cart.EditItem(id, quantity);
            HttpContext.Session.SetObject("cart", cart);

            return RedirectToAction("CheckOut", "Books");
        }
        [HttpPost]
        public RedirectToActionResult RemoveOrderItem(int id)
        {
            ShoppingCart cart = (ShoppingCart)HttpContext.Session.GetObject<ShoppingCart>("cart");
            cart.RemoveItem(id);
            HttpContext.Session.SetObject("cart", cart);

            return RedirectToAction("CheckOut", "Books");
        }

        //public IActionResult ExportMovieList()
        //{
        //    // Get the movie list 
        //    var queryMovie = _context.Book.Include(m => m.Category);
        //    List<Book> movies = queryMovie.ToList();

        //    var stream = new MemoryStream();
        //    using (var xlPackage = new ExcelPackage(stream))
        //    {
        //        var worksheet = xlPackage.Workbook.Worksheets.Add("Movies");
        //        var namedStyle = xlPackage.Workbook.Styles.CreateNamedStyle("HyperLink");
        //        namedStyle.Style.Font.UnderLine = true;
        //        const int startRow = 5;
        //        var row = startRow;

        //        //Create Headers and format them
        //        worksheet.Cells["A1"].Value = "Sample";
        //        using (var r = worksheet.Cells["A1:C1"])
        //        {
        //            r.Merge = true;
        //            r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
        //        }

        //        worksheet.Cells["A4"].Value = "Title";
        //        worksheet.Cells["B4"].Value = "Genre";
        //        worksheet.Cells["C4"].Value = "Price";
        //        worksheet.Cells["A4:C4"].Style.Font.Bold = true;

        //        row = 5;
        //        foreach (var movie in movies)
        //        {
        //            worksheet.Cells[row, 1].Value = movie.Title;
        //            worksheet.Cells[row, 2].Value = movie.Genre.Name;
        //            worksheet.Cells[row, 3].Value = movie.Price;
        //            row++;
        //        }
        //        // save the new spreadsheet
        //        xlPackage.Save();
        //        // Response.Clear();
        //    }
        //    stream.Position = 0;
        //    return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "movies.xlsx");
        //}


    }
}
