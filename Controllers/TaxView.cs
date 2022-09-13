using Microsoft.AspNetCore.Mvc;
using PIN_izračun.DataBase;
using PIN_izračun.Models;

namespace PIN_izračun.Controllers
{
    public class TaxView : Controller
    {
        public IActionResult Index()
        {
            int? id = HttpContext.Session.GetInt32("Id");
            if (id == null)
            {
                // Set Id user if is not null
                return RedirectToAction("Index", "Home");
            }

            TaxCalculationDataBase taxCalculationDataBase = new TaxCalculationDataBase();
            User user = taxCalculationDataBase.GetUser((int)id);

            if (user.Stup)
            {
                user.PrviStup = decimal.Round(user.SalaryBruto * 0.15m, 2);
                user.DrugiStup = decimal.Round(user.SalaryBruto * 0.05m, 2);
            }
            else
            {
                user.PrviStup = decimal.Round(user.SalaryBruto * 0.2m, 2);
            }

            user.Osnovica = decimal.Round(user.SalaryBruto - (user.SalaryBruto * 0.2m), 2);

            int taxReductionId = (int)Enum.Parse(typeof(TaxtReduction), user.TaxReduction);
            user.OlakšicaDjeca = Utils.Utils.CheckNumberTaxReduction(taxReductionId);
            user.Porez = decimal.Round((user.Osnovica - user.OlakšicaDjeca) * 0.2m, 2);

            if (user.Porez < 0)
            {
                user.Porez = 0;
                user.Prirez = 0;
            }
            else
            {
                int cityId = (int)Enum.Parse(typeof(City), user.City);
                user.Prirez = Utils.Utils.CheckNumberByCity(cityId);
                user.Prirez = decimal.Round(user.Porez * user.Prirez, 2);
            }

            user.IznosNeto = decimal.Round(user.Osnovica - user.Porez - user.Prirez, 2);

            taxCalculationDataBase.UpdateTaxNeto(user, (int)id);

            ViewBag.User = user;

            return View("~/Views/TaxView/taxView.cshtml");
        }
    }
}
