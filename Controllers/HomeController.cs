using Microsoft.AspNetCore.Mvc;
using PIN_izračun.DataBase;
using PIN_izračun.Models;
using System.Diagnostics;

namespace PIN_izračun.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("login")]
        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            TaxCalculationDataBase taxCalculationDataBase = new TaxCalculationDataBase();
            password = Utils.Utils.GenerateHashPassword(password); ;

            bool isUserLogged = taxCalculationDataBase.CheckLogIn(username, password);
            //bool isUserLogged = true;
            HttpContext.Session.SetInt32("Id", 1);

            if (!isUserLogged)
            {
                ViewBag.Error = "Krivo ime i/ili pass.";
                return View("Index");
            }

            //return View("~/Views/EditTax/editTax.cshtml");
            return RedirectToAction("Index", "EditTax");
        }

        [Route("register")]
        [HttpPost]
        public IActionResult Register(User user)
        {
            bool hasError = false;
            if (user.Name == null || user.Name.Trim() == "")
            {
                ViewBag.Error += "Ime mora biti upisano.";
                hasError = true;
            }

            if (user.Password == null || user.Password.Trim() == "")
            {
                ViewBag.Error += "Pass mora biti upisan";
                hasError = true;
            }

            if (user.Email == null || user.Email.Trim() == "")
            {
                ViewBag.Error += "Email mora biti upisan";
                hasError = true;
            }

            user.Password = Utils.Utils.GenerateHashPassword(user.Password); ;

            if (hasError == false)
            {
                TaxCalculationDataBase taxCalculationDataBase = new TaxCalculationDataBase();
                bool isUserCreated = taxCalculationDataBase.CreateUser(user);

                if (isUserCreated == false)
                {
                    ViewBag.Error += "Baza nije spremila.";
                }
                else
                {
                    ViewBag.Succes = "Sve OK!";
                }
            }

            return View("index");
        }
    }
}