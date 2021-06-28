using Microsoft.AspNetCore.Mvc;

namespace CompanyAndEmployee.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
