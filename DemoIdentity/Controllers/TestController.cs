using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DemoIdentity.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin, Moderator, Basic")]
    public class TestController : Controller
    {
        // GET: TestController
        public ActionResult Index()
        {
            return Content("Index Page");
        }
        [Authorize(Roles = "SuperAdmin, Admin")]

        // GET: TestController/Details/5
        public ActionResult Details(int id)
        {
            return Content("Detail Page");
        }
        [Authorize(Roles = "SuperAdmin")]
        // GET: TestController/Create
        public ActionResult Create()
        {
            return Content("Create Page");
        }

    }
}
