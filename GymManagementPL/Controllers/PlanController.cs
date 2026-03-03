using GymManagementBLL.Services.Interfaces;
using GymManagementBLL.ViewModels.PlanViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }
        public IActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction("Index");
            }
            var plan =_planService.GetPlanById(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan Not Found";
                RedirectToAction("Index");
            }
            return View(plan);


        }

        public IActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Plan Id";
                return RedirectToAction("Index");
            }
            var plan = _planService.GetPlanToUpdate(id);
            if (plan == null)
            {
                TempData["ErrorMessage"] = "Plan Can Not Be Updated";
                return RedirectToAction("Index");
            }
            return View(plan);
        }
        [HttpPost]
        public IActionResult Edit([FromRoute] int id,UpdatePlanViewModel model)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("WrongData", "Check Missing Fields");
                return View(model);
            }
            var result = _planService.UpdatePlan(model, id);
            if (result)
            {
                TempData["Success"] = "Plan Updated";
            }
            else
            {
                TempData["ErrorMessage"] = "Plan Failed To Update";
            }
            return RedirectToAction("Index");


        }
        [HttpPost]
        public IActionResult Toggle(int id)
        {
            var result = _planService.ToggleStatus(id);
            if (result)
            {
                TempData["Success"] = "Plan Status Changed";
            }
            else
            {
                TempData["ErrorMessage"] = "Failed To Change Status";
            }
            return RedirectToAction("Index");
        }

    }
}
