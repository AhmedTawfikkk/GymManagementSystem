using GymManagementBLL.Services.Classes;
using GymManagementBLL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticService _analyticService;

        public HomeController(IAnalyticService analyticService)
        {
            _analyticService = analyticService;
        }
        public IActionResult Index()
        {
            var data = _analyticService.GetAnalyticsData();
            return View(data);
        }
    }
}
