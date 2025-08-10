using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using ToolsExample.Models;

namespace ToolsExample.Controllers
{
  public class HomeController : Controller
  {
    private readonly ILogger<HomeController> _logger;
    private readonly IValueCalculator calculator;

    private Product[] products =
    {
      new Product {Name = "Kayak", Category = "Watersports", Price = 275M},
      new Product {Name = "Lifejacket", Category = "Watersports", Price = 48.95M},
      new Product {Name = "Soccer ball", Category = "Soccer", Price = 19.50M},
      new Product {Name = "Corner flag", Category = "Soccer", Price = 34.95M}
    };

    public HomeController(ILogger<HomeController> logger, IValueCalculator calculator)
    {
      _logger = logger;
      this.calculator = calculator;
    }

    public ActionResult Index()
    {
      var cart = new ShoppingCart(calculator) { Products = products };
      var totalValue = cart.CalculateProductTotal();
      return View(totalValue);
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
