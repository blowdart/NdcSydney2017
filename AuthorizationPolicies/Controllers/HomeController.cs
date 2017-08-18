using Microsoft.AspNetCore.Mvc;

namespace AuthorizationPolicies.Controllers
{
    public class HomeController : Controller
    {
        private IWeatherProvider _weatherProvider;

        public HomeController(IWeatherProvider weatherProvider)
        {
            _weatherProvider = weatherProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = new Model.HomeViewModel
            {
                Season = _weatherProvider.GetSeason()
            };

            return View(model);
        }

        [HttpGet]
        public IActionResult WeatherPicker()
        {
            var model = new Model.HomeViewModel
            {
                Season = _weatherProvider.GetSeason()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult WeatherPicker(Season season)
        {
            _weatherProvider.SetSeason(season);
            return RedirectToAction("Index");
        }
    }
}