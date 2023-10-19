using FPTBook.Models;
using FPTBook.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FPTBook.Controllers
{
    public class UserListController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserListController(UserManager<ApplicationUser> userManager, ApplicationDbContext context)
        {
            _context = context;
            _userManager = userManager;

        }

        //GET: UsersController
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }

        public ActionResult SetPassword(string id)
        {
            ViewBag.UID = id;
            return View();
        }
        //POST UsersController/SetPassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPassword([Bind("UID, NewPassword, ConfirmPassword")] SetPasswordViewModel pass)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var user = await _userManager.FindByIdAsync(pass.UID);
            //var user = _userManager.Users.FirstOrdefautl(u => u.Id == pass.UID);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{pass.UID}'.");
            }
            await _userManager.RemovePasswordAsync(user);
            var addPasswordResutl = await _userManager.AddPasswordAsync(user, pass.NewPassword);
            if (addPasswordResutl.Succeeded)
            {
                foreach (var error in addPasswordResutl.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);

                }
                return View();
            }
            return RedirectToAction("Index");

            {

            }
        }

    }
}
