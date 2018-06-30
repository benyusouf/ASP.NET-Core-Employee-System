using Microsoft.AspNetCore.Mvc;

namespace SmartTech.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

        public IActionResult FaQs()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }
    }
}