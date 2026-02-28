using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using UniManagementSystem.Domain.Models;
using UniManagementSystem.MVC.Models;

namespace UniManagementSystem.MVC.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
          //  return View(model);  
        }
    }
}
