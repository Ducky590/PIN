using Microsoft.AspNetCore.Mvc;
using PIN_izračun.DataBase;
using PIN_izračun.Models;
using System.Text.RegularExpressions;

namespace PIN_izračun.Controllers
{
    public class EditTaxController : Controller
    {
        public IActionResult Index()
        {
            int? id = HttpContext.Session.GetInt32("Id");
            if (id == null)
            {
                // Set Id user if is not null
                return RedirectToAction("Index", "Home");
            }

            return View("EditTax");
        }

        [Route("CalculateTaxes")]
        [HttpPost]
        public IActionResult CalculateTaxes(User user)
        {
            bool hasError = false;
            Regex regex = new Regex(@"^\d$");
            int? id = HttpContext.Session.GetInt32("Id");

            if (id == null)
            {
                return RedirectToAction("Index", "Home");
            }

            if (regex.IsMatch(user.SalaryBruto.ToString().Replace(".", "")))
            {
                ViewBag.Error += "Upisati samo brojeve";
                hasError = true;
            }

            if (hasError == false)
            {
                int cityId = (int)Enum.Parse(typeof(City), user.City);
                user.Prirez = Utils.Utils.CheckNumberByCity(cityId);

                int taxReductionId = (int)Enum.Parse(typeof(TaxtReduction), user.TaxReduction);
                user.OlakšicaDjeca = Utils.Utils.CheckNumberTaxReduction(taxReductionId);

                TaxCalculationDataBase taxCalculationDataBase = new TaxCalculationDataBase();
                bool isUpdated = taxCalculationDataBase.UpdateTaxSelected(user, (int)id);

                if (isUpdated)
                {
                    return RedirectToAction("Index", "TaxView");
                }
            }

            return View("EditTax");
        }
    }
}
